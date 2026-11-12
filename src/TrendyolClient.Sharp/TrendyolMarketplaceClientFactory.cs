using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Refit;
using TrendyolClient.Sharp.Formatters;
using TrendyolClient.Sharp.Handlers;

namespace TrendyolClient.Sharp;

/// <summary>
///     Creates and caches one set of API clients per seller, environment and storefront. Every <c>GetOrCreate*Client</c> method has two
///     forms: one taking a <see cref="TrendyolCredentials" />, and one taking the same values as separate arguments.
/// </summary>
public class TrendyolMarketplaceClientFactory(IHttpClientFactory httpClientFactory, TrendyolClientConfig config)
{
    private readonly ConcurrentDictionary<(long SellerId, bool Stage, string StoreFront), ClientContainer> _clients = new();

    public TrendyolClientConfig Config { get; } = config;

    /// <summary>
    ///     Client for the Marketplace services. International sellers set <see cref="TrendyolCredentials.StoreFrontCode" /> (DE, SA, AE, KW, GR, SK, RO, CZ, BG):
    ///     it is sent as the <c>storeFrontCode</c> header on every request of this client, so each storefront needs its own client.
    ///     Leave it <c>null</c> for the Türkiye marketplace.
    /// </summary>
    public ITrendyolMarketplaceApi GetOrCreateMarketplaceClient(TrendyolCredentials credentials)
    {
        return GetContainer(credentials)
            .Marketplace.Value;
    }

    /// <inheritdoc cref="GetOrCreateMarketplaceClient(TrendyolCredentials)" />
    public ITrendyolMarketplaceApi GetOrCreateMarketplaceClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false,
        string storeFrontCode = null
    )
    {
        return GetOrCreateMarketplaceClient(new TrendyolCredentials(sellerId, apiKey, apiSecret, useStageApi, storeFrontCode));
    }

    /// <summary>
    ///     Client for the order webhook management services. International sellers set <see cref="TrendyolCredentials.StoreFrontCode" />.
    /// </summary>
    public ITrendyolWebhookApi GetOrCreateWebhookClient(TrendyolCredentials credentials)
    {
        return GetContainer(credentials)
            .Webhook.Value;
    }

    /// <inheritdoc cref="GetOrCreateWebhookClient(TrendyolCredentials)" />
    public ITrendyolWebhookApi GetOrCreateWebhookClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false,
        string storeFrontCode = null
    )
    {
        return GetOrCreateWebhookClient(new TrendyolCredentials(sellerId, apiKey, apiSecret, useStageApi, storeFrontCode));
    }

    /// <summary>
    ///     Client for the finance services (settlements, other financials, payment orders, cargo invoices).
    /// </summary>
    public ITrendyolFinanceApi GetOrCreateFinanceClient(TrendyolCredentials credentials)
    {
        return GetContainer(credentials)
            .Finance.Value;
    }

    /// <inheritdoc cref="GetOrCreateFinanceClient(TrendyolCredentials)" />
    public ITrendyolFinanceApi GetOrCreateFinanceClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetOrCreateFinanceClient(new TrendyolCredentials(sellerId, apiKey, apiSecret, useStageApi));
    }

    /// <summary>
    ///     Client for the International Marketplace (Germany, CEE, GULF): the region specific order, webhook and return services.
    ///     <see cref="TrendyolCredentials.StoreFrontCode" /> (DE, SA, AE, KW, GR, SK, RO, CZ, BG) is required and is sent as the
    ///     <c>storeFrontCode</c> header on every request. Endpoints shared with the Türkiye marketplace (products V2, price and
    ///     inventory, order packages, ...) are on <see cref="GetOrCreateMarketplaceClient(TrendyolCredentials)" />, created with the same storefront code.
    /// </summary>
    public ITrendyolInternationalApi GetOrCreateInternationalClient(TrendyolCredentials credentials)
    {
        ArgumentNullException.ThrowIfNull(credentials);
        if (string.IsNullOrWhiteSpace(credentials.StoreFrontCode))
            throw new ArgumentException("International calls need a storefront code such as DE or SA.", nameof(credentials));
        return GetContainer(credentials)
            .International.Value;
    }

    /// <inheritdoc cref="GetOrCreateInternationalClient(TrendyolCredentials)" />
    public ITrendyolInternationalApi GetOrCreateInternationalClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        string storeFrontCode,
        bool useStageApi = false
    )
    {
        return GetOrCreateInternationalClient(new TrendyolCredentials(sellerId, apiKey, apiSecret, useStageApi, storeFrontCode));
    }

    /// <summary>
    ///     Client for the Global Partner (Global Center Gateway, <c>/integration/exportcentergw</c>) API.
    /// </summary>
    public ITrendyolGlobalPartnerApi GetOrCreateGlobalPartnerClient(TrendyolCredentials credentials)
    {
        return GetContainer(credentials)
            .GlobalPartner.Value;
    }

    /// <inheritdoc cref="GetOrCreateGlobalPartnerClient(TrendyolCredentials)" />
    public ITrendyolGlobalPartnerApi GetOrCreateGlobalPartnerClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetOrCreateGlobalPartnerClient(new TrendyolCredentials(sellerId, apiKey, apiSecret, useStageApi));
    }

    /// <summary>
    ///     Client for the Export Center (AutoFT) API under <c>/integration/ecgw</c>. Kept separate from the marketplace client
    ///     because it reuses method names such as <c>CreateProductsAsync</c> with a different contract.
    /// </summary>
    public ITrendyolExportApi GetOrCreateExportClient(TrendyolCredentials credentials)
    {
        return GetContainer(credentials)
            .Export.Value;
    }

    /// <inheritdoc cref="GetOrCreateExportClient(TrendyolCredentials)" />
    public ITrendyolExportApi GetOrCreateExportClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetOrCreateExportClient(new TrendyolCredentials(sellerId, apiKey, apiSecret, useStageApi));
    }

    /// <summary>
    ///     Drops the cached clients of a seller (every environment and storefront), e.g. after regenerating API credentials.
    /// </summary>
    public void InvalidateClient(long sellerId)
    {
        foreach (var key in _clients.Keys.Where(k => k.SellerId == sellerId)) _clients.TryRemove(key, out _);
    }

    private ClientContainer GetContainer(TrendyolCredentials credentials)
    {
        ArgumentNullException.ThrowIfNull(credentials);

        var storeFront = string.IsNullOrWhiteSpace(credentials.StoreFrontCode) ? string.Empty : credentials.StoreFrontCode.Trim().ToUpperInvariant();
        var key = (credentials.SellerId, credentials.UseStageApi, storeFront);
        if (_clients.TryGetValue(key, out var existingContainer))
        {
            // New key or secret for a known seller: rebuild the client
            if (existingContainer.ApiKey == credentials.ApiKey && existingContainer.ApiSecret == credentials.ApiSecret) return existingContainer;

            _clients.TryRemove(key, out _);
        }

        return _clients.GetOrAdd(key,
            _ => new ClientContainer(credentials.SellerId, credentials.ApiKey, credentials.ApiSecret, credentials.UseStageApi, storeFront, httpClientFactory, Config));
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
            var httpClient = httpClientFactory.CreateClient(TrendyolInternals.HttpClientName);
            httpClient.BaseAddress = new Uri(baseUrl);

            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("tr-TR"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", $"{sellerId} - {config.IntegrationName}");
            httpClient.DefaultRequestHeaders.Add(TrendyolInternals.SellerIdHeader, sellerId.ToString());
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