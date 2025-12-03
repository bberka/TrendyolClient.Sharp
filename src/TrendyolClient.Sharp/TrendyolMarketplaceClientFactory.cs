using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Exceptions;
using TrendyolClient.Sharp.Formatters;

namespace TrendyolClient.Sharp;

public class TrendyolMarketplaceClientFactory(IHttpClientFactory httpClientFactory, TrendyolClientConfig config)
{
    // The dictionary now holds a 'ClientContainer' instead of a Tuple.
    private readonly ConcurrentDictionary<long, ClientContainer> _clients = new();

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

    public void InvalidateClient(long sellerId)
    {
        _clients.TryRemove(sellerId, out _);
    }

    private ClientContainer GetContainer(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi
    )
    {
        if (_clients.TryGetValue(sellerId, out var existingContainer))
        {
            // Validate credentials
            if (existingContainer.ApiKey == apiKey && existingContainer.ApiSecret == apiSecret) return existingContainer;

            InvalidateClient(sellerId);
        }

        return _clients.GetOrAdd(sellerId, new ClientContainer(sellerId, apiKey, apiSecret, useStageApi, httpClientFactory, Config));
    }

    // -------------------------------------------------------------------------
    // Static Exception Helpers (Pure Functions)
    // -------------------------------------------------------------------------

    private static Func<HttpResponseMessage, Task<Exception>> CreateExceptionFactory(long sellerId)
    {
        return async response =>
        {
            if (response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => new TrendyolAuthenticationException(
                    $"Authentication failed for seller {sellerId}. Verify API credentials.", sellerId),

                HttpStatusCode.Forbidden => new TrendyolAuthenticationException($"Access forbidden for seller {sellerId}.", sellerId),

                HttpStatusCode.NotFound => new TrendyolNotFoundException("Resource", response.RequestMessage?.RequestUri?.PathAndQuery ?? "unknown"),

                HttpStatusCode.TooManyRequests => new TrendyolRateLimitException("Rate limit exceeded.", response.Headers.RetryAfter?.Date),

                HttpStatusCode.BadRequest => ParseBadRequestException(content),

                HttpStatusCode.Conflict => new TrendyolConflictException("Resource conflict.", content),

                HttpStatusCode.UnprocessableEntity => new TrendyolBusinessRuleException("Business rule validation failed."),

                HttpStatusCode.InternalServerError or HttpStatusCode.BadGateway or HttpStatusCode.ServiceUnavailable => new TrendyolServerException(
                    $"Trendyol server error: {response.StatusCode}", (int)response.StatusCode, content),

                _ => new TrendyolRequestFailedException($"Request failed: {response.StatusCode} {response.ReasonPhrase}")
            };
        };
    }

    private static Exception ParseBadRequestException(string content)
    {
        try
        {
            if (!string.IsNullOrEmpty(content) && content.Contains("errors"))
                return new TrendyolValidationException("Request", "Invalid parameters. " + content);
        }
        catch
        {
            /* Ignore parse errors */
        }

        return new TrendyolValidationException("Request", "Invalid request parameters");
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
                ExceptionFactory = CreateExceptionFactory(sellerId)
            };

            // 3. Define Lazy Initializers
            // The code inside the lambda () => ... is NOT executed yet.
            Marketplace = new Lazy<ITrendyolMarketplaceApi>(() => RestService.For<ITrendyolMarketplaceApi>(httpClient, refitSettings));
            Webhook = new Lazy<ITrendyolWebhookApi>(() => RestService.For<ITrendyolWebhookApi>(httpClient, refitSettings));
            Finance = new Lazy<ITrendyolFinanceApi>(() => RestService.For<ITrendyolFinanceApi>(httpClient, refitSettings));
        }

        public string ApiKey { get; }
        public string ApiSecret { get; }

        // Lazy objects ensure Refit creation logic only runs when specifically requested
        public Lazy<ITrendyolMarketplaceApi> Marketplace { get; }
        public Lazy<ITrendyolWebhookApi> Webhook { get; }
        public Lazy<ITrendyolFinanceApi> Finance { get; }
    }
}