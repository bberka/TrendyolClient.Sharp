using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Exceptions;

namespace TrendyolClient.Sharp.Handlers;

/// <summary>
///     Turns non-success HTTP responses into the typed <see cref="TrendyolException" /> hierarchy.
///     Registered only when <see cref="TrendyolClientConfig.ThrowOnError" /> is enabled.
/// </summary>
internal sealed class TrendyolErrorMappingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
        if (response.IsSuccessStatusCode) return response;

        long? sellerId = null;
        if (request.Headers.TryGetValues("X-Internal-Seller-Id", out var values) && long.TryParse(values.FirstOrDefault(), out var parsed)) sellerId = parsed;

        var content = response.Content == null
            ? string.Empty
            : await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);

        var exception = Map(response, content, sellerId, request.RequestUri?.PathAndQuery ?? "unknown");
        response.Dispose();
        throw exception;
    }

    internal static TrendyolException Map(HttpResponseMessage response, string content, long? sellerId, string path)
    {
        var seller = sellerId.HasValue ? $" for seller {sellerId}" : string.Empty;
        return response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => new TrendyolAuthenticationException($"Authentication failed{seller}. Verify API key and secret.", sellerId),

            // Per Trendyol's error table: wrong endpoint/parameters, or a request without a User-Agent header.
            HttpStatusCode.Forbidden => new TrendyolAuthenticationException(
                $"Access forbidden{seller}. Check the endpoint, the parameters and the User-Agent header.", sellerId),

            HttpStatusCode.NotFound => new TrendyolNotFoundException("Resource", path),

            HttpStatusCode.TooManyRequests => new TrendyolRateLimitException("Rate limit exceeded (50 requests per 10 seconds per endpoint).",
                response.Headers.RetryAfter?.Date ?? (response.Headers.RetryAfter?.Delta is { } delta ? DateTimeOffset.UtcNow.Add(delta) : null)),

            HttpStatusCode.BadRequest => new TrendyolValidationException("Request",
                string.IsNullOrWhiteSpace(content) ? "Invalid request parameters" : "Invalid parameters. " + content),

            HttpStatusCode.Conflict => new TrendyolConflictException("Resource conflict.", content),

            HttpStatusCode.UnprocessableEntity => new TrendyolBusinessRuleException("Business rule validation failed.",
                errorDetails: new() { ["response"] = content }),

            HttpStatusCode.InternalServerError or HttpStatusCode.BadGateway or HttpStatusCode.ServiceUnavailable or HttpStatusCode.GatewayTimeout =>
                new TrendyolServerException($"Trendyol server error: {(int)response.StatusCode} {response.StatusCode}", (int)response.StatusCode, content),

            _ => new TrendyolRequestFailedException($"Request failed: {(int)response.StatusCode} {response.ReasonPhrase}", (int)response.StatusCode, content)
        };
    }
}
