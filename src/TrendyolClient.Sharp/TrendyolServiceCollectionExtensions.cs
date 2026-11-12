using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using TrendyolClient.Sharp.Handlers;

namespace TrendyolClient.Sharp;

public static class TrendyolServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the Trendyol client factory to the dependency injection container.
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configure">Optional configuration action</param>
    /// <returns>
    ///     The builder of the <c>HttpClient</c> every Trendyol client is built on. Chain your own handlers on it, for example
    ///     <c>.AddHttpMessageHandler&lt;MyThrottlingHandler&gt;()</c> or <c>.AddStandardResilienceHandler()</c>: the library itself does not
    ///     retry, throttle or validate. Your handlers see the request before it is sent and can read the seller id from the
    ///     <c>X-Internal-Seller-Id</c> request header, which is removed just before the request leaves.
    /// </returns>
    public static IHttpClientBuilder AddTrendyolApiClient(this IServiceCollection services, Action<TrendyolClientConfig> configure = null)
    {
        var config = new TrendyolClientConfig();
        configure?.Invoke(config);
        services.AddSingleton(config);

        services.AddTransient<TrendyolLoggingHandler>();
        services.AddTransient<TrendyolSellerIdInjectorMessageHandler>();
        services.AddTransient<TrendyolErrorMappingHandler>();

        var httpClientBuilder = services.AddHttpClient(TrendyolInternals.HttpClientName)
            .ConfigurePrimaryHttpMessageHandler(() => CreatePrimaryHandler())
            .AddHttpMessageHandler<TrendyolSellerIdInjectorMessageHandler>();

        // Sits outside the logging handler, so the raw failure is logged before it is turned into an exception
        if (config.ThrowOnError) httpClientBuilder.AddHttpMessageHandler<TrendyolErrorMappingHandler>();

        if (config.EnableLogging) httpClientBuilder.AddHttpMessageHandler<TrendyolLoggingHandler>();

        httpClientBuilder.ConfigureHttpClient(client => { client.Timeout = TimeSpan.FromSeconds(config.RequestTimeoutSeconds); });

        services.AddSingleton<TrendyolMarketplaceClientFactory>();

        return httpClientBuilder;
    }

    /// <summary>
    ///     Registers the clients of one fixed seller, so they can be injected directly (<see cref="ITrendyolMarketplaceApi" />,
    ///     <see cref="ITrendyolWebhookApi" />, <see cref="ITrendyolFinanceApi" />, the Export, Global Partner and International clients, and the
    ///     per-area interfaces such as <see cref="ITrendyolOrderApi" />). Call <c>AddTrendyolApiClient</c> as well.
    ///     The clients are singletons: for several sellers, or credentials that change at runtime, use
    ///     <see cref="TrendyolMarketplaceClientFactory" /> instead.
    ///     The International client needs <see cref="TrendyolCredentials.StoreFrontCode" />; resolving it without one throws.
    /// </summary>
    public static IServiceCollection AddTrendyolSeller(this IServiceCollection services, TrendyolCredentials credentials)
    {
        ArgumentNullException.ThrowIfNull(credentials);
        return services.AddTrendyolSeller(_ => credentials);
    }

    /// <summary>
    ///     Like <see cref="AddTrendyolSeller(IServiceCollection, TrendyolCredentials)" />, but reads the credentials when a client is first
    ///     resolved, for example from <c>IConfiguration</c>, <c>IOptions</c> or a secret store.
    /// </summary>
    public static IServiceCollection AddTrendyolSeller(this IServiceCollection services, Func<IServiceProvider, TrendyolCredentials> credentials)
    {
        ArgumentNullException.ThrowIfNull(credentials);

        TrendyolMarketplaceClientFactory Factory(IServiceProvider sp)
        {
            return sp.GetRequiredService<TrendyolMarketplaceClientFactory>();
        }

        services.AddSingleton(sp => Factory(sp).GetOrCreateMarketplaceClient(credentials(sp)));
        services.AddSingleton(sp => Factory(sp).GetOrCreateWebhookClient(credentials(sp)));
        services.AddSingleton(sp => Factory(sp).GetOrCreateFinanceClient(credentials(sp)));
        services.AddSingleton(sp => Factory(sp).GetOrCreateExportClient(credentials(sp)));
        services.AddSingleton(sp => Factory(sp).GetOrCreateGlobalPartnerClient(credentials(sp)));
        services.AddSingleton(sp => Factory(sp).GetOrCreateInternationalClient(credentials(sp)));

        // The marketplace client implements every area interface; register them so a consumer can inject (and fake) just the area it uses.
        services.AddSingleton<ITrendyolProductApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolOrderApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolClaimApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolQnAApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolMemberApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolCommonLabelApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolSellerApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolInvoiceApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolCompensationApi>(sp => sp.GetRequiredService<ITrendyolMarketplaceApi>());
        services.AddSingleton<ITrendyolInternationalOrderApi>(sp => sp.GetRequiredService<ITrendyolInternationalApi>());
        services.AddSingleton<ITrendyolInternationalClaimApi>(sp => sp.GetRequiredService<ITrendyolInternationalApi>());

        return services;
    }

    /// <summary>
    ///     The primary handler: a cookie-less-by-default <see cref="HttpClientHandler" /> with decompression, wrapped so the internal seller
    ///     id header is removed after all other handlers have run. <paramref name="inner" /> replaces the network handler in tests.
    /// </summary>
    internal static HttpMessageHandler CreatePrimaryHandler(HttpMessageHandler inner = null)
    {
        return new TrendyolWireCleanupHandler
        {
            InnerHandler = inner ?? new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }
        };
    }
}
