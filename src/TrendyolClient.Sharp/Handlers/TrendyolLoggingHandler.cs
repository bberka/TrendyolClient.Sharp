using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TrendyolClient.Sharp.Handlers;

public class TrendyolLoggingHandler(ILogger<TrendyolLoggingHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        logger.LogInformation("Trendyol API Request: {Method} {Uri}", request.Method, request.RequestUri);

        try
        {
            var response = await base.SendAsync(request, cancellationToken);

            stopwatch.Stop();

            logger.LogInformation("Trendyol API Response: {StatusCode} in {ElapsedMs}ms", response.StatusCode, stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            logger.LogError(ex, "Trendyol API Error: {Method} {Uri} failed after {ElapsedMs}ms", request.Method, request.RequestUri,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }
}