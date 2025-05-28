using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TrendyolClient.Sharp
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddTrendyolClientFactory(this IServiceCollection services,Action<TrendyolClientConfig> configure = null) {
      services.AddHttpClient("TrendyolClient")
              .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler {
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
              })
              .AddHttpMessageHandler<SellerIdInjectorMessageHandler>();

      if (configure is not null) {
        var config = new TrendyolClientConfig();
        configure(config);
        services.AddSingleton(config);
      }
      services.AddSingleton<SellerIdInjectorMessageHandler>();
      services.AddSingleton<TrendyolMarketplaceClientFactory>();

      return services;
    }
  }
}