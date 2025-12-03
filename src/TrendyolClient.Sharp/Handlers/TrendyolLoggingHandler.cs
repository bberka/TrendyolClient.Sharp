using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TrendyolClient.Sharp.Handlers;

public class TrendyolLoggingHandler(ILogger<TrendyolLoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestId = Guid.NewGuid()
            .ToString("N")[..8];
        var stopwatch = Stopwatch.StartNew();

        // Extract seller ID if available for better log context
        string sellerId = null;
        if (request.Headers.TryGetValues("X-Internal-Seller-Id", out var sellerIdValues)) sellerId = sellerIdValues.FirstOrDefault();

        logger.LogInformation("[{RequestId}] Trendyol API Request: {Method} {Uri} {SellerContext}", requestId, request.Method,
            request.RequestUri?.PathAndQuery, sellerId != null ? $"[Seller: {sellerId}]" : string.Empty);

        try
        {
            var response = await base.SendAsync(request, cancellationToken);
            stopwatch.Stop();

            if (response.IsSuccessStatusCode)
                logger.LogInformation("[{RequestId}] Trendyol API Success: {StatusCode} in {ElapsedMs}ms", requestId, (int)response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            else
                logger.LogWarning("[{RequestId}] Trendyol API Failed: {StatusCode} {ReasonPhrase} in {ElapsedMs}ms", requestId,
                    (int)response.StatusCode, response.ReasonPhrase, stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            stopwatch.Stop();
            logger.LogError("[{RequestId}] Trendyol API Timeout: {Method} {Uri} after {ElapsedMs}ms", requestId, request.Method,
                request.RequestUri?.PathAndQuery, stopwatch.ElapsedMilliseconds);
            throw;
        }
        catch (HttpRequestException ex)
        {
            stopwatch.Stop();
            logger.LogError(ex, "[{RequestId}] Trendyol API Network Error: {Method} {Uri} failed after {ElapsedMs}ms", requestId, request.Method,
                request.RequestUri?.PathAndQuery, stopwatch.ElapsedMilliseconds);
            throw;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            logger.LogError(ex, "[{RequestId}] Trendyol API Error: {Method} {Uri} failed after {ElapsedMs}ms", requestId, request.Method,
                request.RequestUri?.PathAndQuery, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}