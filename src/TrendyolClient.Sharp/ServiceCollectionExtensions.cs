using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TrendyolClient.Sharp
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddTrendyolClientFactory(this IServiceCollection services) {
      services.AddHttpClient("TrendyolClient")
              .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler {
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
              });

      services.AddSingleton<TrendyolMarketplaceClientFactory>();

      return services;
    }
  }
}