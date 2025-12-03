using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Exceptions;
using TrendyolClient.Sharp.Extensions;
using TrendyolClient.Sharp.Formatters;

namespace TrendyolClient.Sharp;

public class TrendyolMarketplaceClientFactory(IHttpClientFactory httpClientFactory, TrendyolClientConfig config)
{
    // Store API clients along with credentials for validation
    private readonly ConcurrentDictionary<long, (ITrendyolMarketplaceApi marketplace, ITrendyolWebhookApi webhook, string apiKey, string apiSecret)>
        _clients = new();

    public TrendyolClientConfig Config { get; } = config;

    /// <summary>
    /// Gets or creates a Marketplace API client for the specified seller
    /// </summary>
    public ITrendyolMarketplaceApi GetOrCreateClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetOrCreateClients(sellerId, apiKey, apiSecret, useStageApi)
            .marketplace;
    }

    /// <summary>
    /// Gets or creates a Webhook API client for the specified seller
    /// </summary>
    public ITrendyolWebhookApi GetOrCreateWebhookClient(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        return GetOrCreateClients(sellerId, apiKey, apiSecret, useStageApi)
            .webhook;
    }

    /// <summary>
    /// Gets or creates both Marketplace and Webhook API clients for the specified seller
    /// </summary>
    public (ITrendyolMarketplaceApi marketplace, ITrendyolWebhookApi webhook) GetOrCreateClients(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        if (_clients.TryGetValue(sellerId, out var existingClients))
        {
            if (existingClients.apiKey == apiKey && existingClients.apiSecret == apiSecret)
            {
                return (existingClients.marketplace, existingClients.webhook);
            }

            InvalidateClient(sellerId);
        }

        return _clients.GetOrAdd(sellerId, _ =>
            {
                var (marketplace, webhook) = CreateClients(sellerId, apiKey, apiSecret, useStageApi);
                return (marketplace, webhook, apiKey, apiSecret);
            })
            .AsValueTuple();
    }

    public void InvalidateClient(long sellerId)
    {
        _clients.TryRemove(sellerId, out _);
    }

    protected (ITrendyolMarketplaceApi marketplace, ITrendyolWebhookApi webhook) CreateClients(
        long sellerId,
        string apiKey,
        string apiSecret,
        bool useStageApi = false
    )
    {
        var baseUrl = useStageApi ? "https://stageapigw.trendyol.com" : "https://apigw.trendyol.com";

        var httpClient = httpClientFactory.CreateClient("TrendyolClient");
        httpClient.BaseAddress = new Uri(baseUrl);

        // Set authentication header dynamically
        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
        httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("tr-TR"));
        httpClient.DefaultRequestHeaders.Add("User-Agent", $"{sellerId} - {Config.IntegrationName}");
        httpClient.DefaultRequestHeaders.Add("X-Internal-Seller-Id", sellerId.ToString());

        var refitSettings = new RefitSettings
        {
            UrlParameterKeyFormatter = new LowerCaseFirstCharParameterFormatter(),
            ExceptionFactory = CreateExceptionFactory(sellerId)
        };

        var marketplace = RestService.For<ITrendyolMarketplaceApi>(httpClient, refitSettings);
        var webhook = RestService.For<ITrendyolWebhookApi>(httpClient, refitSettings);

        return (marketplace, webhook);
    }

    /// <summary>
    /// Creates a Refit exception factory that maps HTTP status codes to custom exceptions
    /// </summary>
    private static Func<HttpResponseMessage, Task<Exception>> CreateExceptionFactory(long sellerId)
    {
        return async response =>
        {
            var content = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => new TrendyolAuthenticationException(
                    $"Authentication failed for seller {sellerId}. Please verify your API credentials.", sellerId),

                HttpStatusCode.Forbidden => new TrendyolAuthenticationException(
                    $"Access forbidden for seller {sellerId}. Check your API permissions.", sellerId),

                HttpStatusCode.NotFound => new TrendyolNotFoundException("Resource", response.RequestMessage?.RequestUri?.PathAndQuery ?? "unknown"),

                HttpStatusCode.TooManyRequests => new TrendyolRateLimitException("Rate limit exceeded. Please retry later.",
                    response.Headers.RetryAfter?.Date),

                HttpStatusCode.BadRequest => ParseBadRequestException(content),

                HttpStatusCode.Conflict => new TrendyolConflictException("The request conflicts with the current state of the resource.", content),

                HttpStatusCode.UnprocessableEntity => new TrendyolBusinessRuleException("Business rule validation failed.", null, null),

                HttpStatusCode.InternalServerError or HttpStatusCode.BadGateway or HttpStatusCode.ServiceUnavailable or HttpStatusCode.GatewayTimeout
                    => new TrendyolServerException($"Trendyol server error: {response.StatusCode}", (int)response.StatusCode, content),

                _ => new TrendyolRequestFailedException($"Request failed with status {response.StatusCode}: {response.ReasonPhrase}")
            };
        };
    }

    /// <summary>
    /// Attempts to parse validation errors from bad request response
    /// </summary>
    private static Exception ParseBadRequestException(string content)
    {
        // Try to parse Trendyol's error format
        // Trendyol typically returns: {"message": "Validation failed", "errors": [...]}
        try
        {
            if (!string.IsNullOrEmpty(content) && content.Contains("errors"))
            {
                return new TrendyolValidationException("Request", "Invalid request parameters. " + content);
            }
        }
        catch
        {
            // If parsing fails, return generic validation exception
        }

        return new TrendyolValidationException("Request", "Invalid request parameters");
    }
}