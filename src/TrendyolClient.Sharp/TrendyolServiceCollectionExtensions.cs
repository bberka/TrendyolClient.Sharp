using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using TrendyolClient.Sharp.Handlers;

namespace TrendyolClient.Sharp;

public static class TrendyolServiceCollectionExtensions
{
    /// <summary>
    ///     Adds TrendyolClient services to the dependency injection container
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configure">Optional configuration action</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddTrendyolApiClient(this IServiceCollection services, Action<TrendyolClientConfig> configure = null)
    {
        // Configure TrendyolClient config
        var config = new TrendyolClientConfig();
        configure?.Invoke(config);
        services.AddSingleton(config);

        // Register handlers
        services.AddTransient<TrendyolLoggingHandler>();
        services.AddTransient<TrendyolSellerIdInjectorMessageHandler>();

        // Configure HttpClient with handlers
        var httpClientBuilder = services.AddHttpClient("TrendyolClient")
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            })
            .AddHttpMessageHandler<TrendyolSellerIdInjectorMessageHandler>();

        // Add logging handler if enabled
        if (config.EnableLogging) httpClientBuilder.AddHttpMessageHandler<TrendyolLoggingHandler>();

        // Configure timeout
        httpClientBuilder.ConfigureHttpClient(client => { client.Timeout = TimeSpan.FromSeconds(config.RequestTimeoutSeconds); });

        // Register the client factory
        services.AddSingleton<TrendyolMarketplaceClientFactory>();

        return services;
    }
}