using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Refit;
using TrendyolClient.Sharp.Formatters;

namespace TrendyolClient.Sharp;

public class TrendyolMarketplaceClientFactory(IHttpClientFactory httpClientFactory, TrendyolClientConfig config)
{
    // The dictionary now holds a 'ClientContainer' instead of a Tuple.
    private readonly ConcurrentDictionary<(long SellerId, bool Stage, string StoreFront), ClientContainer> _clients = new();

    public TrendyolClientConfig Config { get; } = config;

    /// <summary>
    ///     Client for the Marketplace services. International sellers pass their <paramref name="storeFrontCode" /> (DE, SA, AE, KW, GR, SK, RO, CZ, BG):
    ///     it is sent as the <c>storeFrontCode</c> header on every request of this client, so each storefront needs its own client.
    ///     Leave it <c>null</c> for the Türkiye marketplace.
    /// </summary>
    public ITrendyolMarketplaceApi GetOrCreateMarketplaceClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false,
        string storeFrontCode = null
    )
    {
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi, storeFrontCode)
            .Marketplace.Value;
    }

    /// <summary>
    ///     Client for the order webhook management services. International sellers pass their <paramref name="storeFrontCode" />.
    /// </summary>
    public ITrendyolWebhookApi GetOrCreateWebhookClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false,
        string storeFrontCode = null
    )
    {
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi, storeFrontCode)
            .Webhook.Value;
    }

    public ITrendyolFinanceApi GetOrCreateFinanceClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi)
            .Finance.Value;
    }

    /// <summary>
    ///     Client for the International Marketplace (Germany, CEE, GULF): the region specific order, webhook and return services.
    ///     <paramref name="storeFrontCode" /> (DE, SA, AE, KW, GR, SK, RO, CZ, BG) is sent as the <c>storeFrontCode</c> header on every request.
    ///     Endpoints shared with the Türkiye marketplace (products V2, price and inventory, order packages, ...) are on
    ///     <see cref="GetOrCreateMarketplaceClient" />, created with the same storefront code.
    /// </summary>
    public ITrendyolInternationalApi GetOrCreateInternationalClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        string storeFrontCode,
        bool useStageApi = false
    )
    {
        if (string.IsNullOrWhiteSpace(storeFrontCode)) throw new ArgumentException("International calls need a storefront code such as DE or SA.", nameof(storeFrontCode));
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi, storeFrontCode)
            .International.Value;
    }

    /// <summary>
    ///     Client for the Global Partner (Global Center Gateway, <c>/integration/exportcentergw</c>) API.
    /// </summary>
    public ITrendyolGlobalPartnerApi GetOrCreateGlobalPartnerClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi)
            .GlobalPartner.Value;
    }

    /// <summary>
    ///     Client for the Export Center (AutoFT) API under <c>/integration/ecgw</c>. Kept separate from the marketplace client
    ///     because it reuses method names such as <c>CreateProductsAsync</c> with a different contract.
    /// </summary>
    public ITrendyolExportApi GetOrCreateExportClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi)
            .Export.Value;
    }

    /// <summary>
    ///     Drops the cached clients of a seller (production and stage), e.g. after regenerating API credentials.
    /// </summary>
    public void InvalidateClient(long sellerId)
    {
        foreach (var key in _clients.Keys.Where(k => k.SellerId == sellerId)) _clients.TryRemove(key, out _);
    }

    private ClientContainer GetContainer(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi,
        string storeFrontCode = null
    )
    {
        var storeFront = string.IsNullOrWhiteSpace(storeFrontCode) ? string.Empty : storeFrontCode.Trim().ToUpperInvariant();
        var key = (sellerId, useStageApi, storeFront);
        if (_clients.TryGetValue(key, out var existingContainer))
        {
            // Validate credentials
            if (existingContainer.ApiKey == apiKey && existingContainer.ApiSecret == apiSecret) return existingContainer;

            _clients.TryRemove(key, out _);
        }

        return _clients.GetOrAdd(key, _ => new ClientContainer(sellerId, apiKey, apiSecret, useStageApi, storeFront, httpClientFactory, Config));
    }

    /// <summary>
    ///     Internal container that holds shared HTTP infrastructure and Lazy-loaded clients.
    /// </summary>
    private class ClientContainer
    {
        public ClientContainer(
            long sellerId,
            string apiKey,
            string apiSecret,
            bool useStageApi,
            string storeFrontCode,
            IHttpClientFactory httpClientFactory,
            TrendyolClientConfig config
        )
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;

            // 1. Setup Shared HttpClient
            var baseUrl = useStageApi ? "https://stageapigw.trendyol.com" : "https://apigw.trendyol.com";
            var httpClient = httpClientFactory.CreateClient("TrendyolClient");
            httpClient.BaseAddress = new Uri(baseUrl);

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("tr-TR"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", $"{sellerId} - {config.IntegrationName}");
            httpClient.DefaultRequestHeaders.Add("X-Internal-Seller-Id", sellerId.ToString());
            if (storeFrontCode.Length > 0) httpClient.DefaultRequestHeaders.Add("storeFrontCode", storeFrontCode);

            // 2. Setup Shared Refit Settings
            var refitSettings = new RefitSettings
            {
                UrlParameterKeyFormatter = new LowerCaseFirstCharParameterFormatter(),
                ContentSerializer = TrendyolJson.CreateContentSerializer()
            };

            // 3. Define Lazy Initializers
            // The code inside the lambda () => ... is NOT executed yet.
            Marketplace = new Lazy<ITrendyolMarketplaceApi>(() => RestService.For<ITrendyolMarketplaceApi>(httpClient, refitSettings));
            Webhook = new Lazy<ITrendyolWebhookApi>(() => RestService.For<ITrendyolWebhookApi>(httpClient, refitSettings));
            Finance = new Lazy<ITrendyolFinanceApi>(() => RestService.For<ITrendyolFinanceApi>(httpClient, refitSettings));
            Export = new Lazy<ITrendyolExportApi>(() => RestService.For<ITrendyolExportApi>(httpClient, refitSettings));
            International = new Lazy<ITrendyolInternationalApi>(() => RestService.For<ITrendyolInternationalApi>(httpClient, refitSettings));
            GlobalPartner = new Lazy<ITrendyolGlobalPartnerApi>(() => RestService.For<ITrendyolGlobalPartnerApi>(httpClient, refitSettings));
        }

        public string ApiKey { get; }
        public string ApiSecret { get; }

        // Lazy objects ensure Refit creation logic only runs when specifically requested
        public Lazy<ITrendyolMarketplaceApi> Marketplace { get; }
        public Lazy<ITrendyolWebhookApi> Webhook { get; }
        public Lazy<ITrendyolFinanceApi> Finance { get; }
        public Lazy<ITrendyolExportApi> Export { get; }
        public Lazy<ITrendyolInternationalApi> International { get; }
        public Lazy<ITrendyolGlobalPartnerApi> GlobalPartner { get; }
    }
}