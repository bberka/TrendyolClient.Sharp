using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Formatters;

namespace TrendyolClient.Sharp;

public class TrendyolMarketplaceClientFactory(IHttpClientFactory httpClientFactory, TrendyolClientConfig config)
{
    // The dictionary now holds a 'ClientContainer' instead of a Tuple.
    private readonly ConcurrentDictionary<(long SellerId, bool Stage), ClientContainer> _clients = new();

    public TrendyolClientConfig Config { get; } = config;

    public ITrendyolMarketplaceApi GetOrCreateMarketplaceClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi)
            .Marketplace.Value;
    }

    public ITrendyolWebhookApi GetOrCreateWebhookClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetContainer(sellerId, apiKey, apiSecret, useStageApi)
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
        _clients.TryRemove((sellerId, false), out _);
        _clients.TryRemove((sellerId, true), out _);
    }

    private ClientContainer GetContainer(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi
    )
    {
        var key = (sellerId, useStageApi);
        if (_clients.TryGetValue(key, out var existingContainer))
        {
            // Validate credentials
            if (existingContainer.ApiKey == apiKey && existingContainer.ApiSecret == apiSecret) return existingContainer;

            _clients.TryRemove(key, out _);
        }

        return _clients.GetOrAdd(key, _ => new ClientContainer(sellerId, apiKey, apiSecret, useStageApi, httpClientFactory, Config));
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
        }

        public string ApiKey { get; }
        public string ApiSecret { get; }

        // Lazy objects ensure Refit creation logic only runs when specifically requested
        public Lazy<ITrendyolMarketplaceApi> Marketplace { get; }
        public Lazy<ITrendyolWebhookApi> Webhook { get; }
        public Lazy<ITrendyolFinanceApi> Finance { get; }
        public Lazy<ITrendyolExportApi> Export { get; }
    }
}