using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Refit;

namespace TrendyolClient.Sharp
{
  public class TrendyolMarketplaceClientFactory
  {
    private readonly IHttpClientFactory _httpClientFactory;

    // Store API clients along with credentials for validation
    private readonly ConcurrentDictionary<long, (ITrendyolMarketplaceApi client, string apiKey, string apiSecret)> _clients = new();

    public TrendyolMarketplaceClientFactory(IHttpClientFactory httpClientFactory) {
      _httpClientFactory = httpClientFactory;
    }

    public ITrendyolMarketplaceApi GetOrCreateClient(long sellerId,
                                                     string apiKey,
                                                     string apiSecret,
                                                     bool useStageApi = false,
                                                     string integrationCompanyNameForHeader = "SelfIntegration") {
      if (_clients.TryGetValue(sellerId, out var existingClient)) {
        if (existingClient.apiKey == apiKey && existingClient.apiSecret == apiSecret) {
          return existingClient.client; 
        }

        InvalidateClient(sellerId);
      }

      return _clients.GetOrAdd(sellerId, _ => {
        var newClient = CreateClient(sellerId, apiKey, apiSecret, useStageApi, integrationCompanyNameForHeader);
        return (newClient, apiKey, apiSecret); // Store client with credentials
      }).client;
    }

    public void InvalidateClient(long sellerId) {
      _clients.TryRemove(sellerId, out _);
    }

    protected ITrendyolMarketplaceApi CreateClient(long sellerId,
                                                   string apiKey,
                                                   string apiSecret,
                                                   bool useStageApi = false,
                                                   string integrationCompanyNameForHeader = "SelfIntegration") {
      var baseUrl = useStageApi
                      ? "https://stageapigw.trendyol.com"
                      : "https://apigw.trendyol.com";

      var httpClient = _httpClientFactory.CreateClient("TrendyolClient");
      httpClient.BaseAddress = new Uri(baseUrl);

      // Set authentication header dynamically
      var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiKey}:{apiSecret}"));
      httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
      httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
      httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
      httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("tr-TR"));
      httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"{sellerId} - {integrationCompanyNameForHeader}"));

      httpClient.DefaultRequestHeaders.Add("X-Internal-Seller-Id", sellerId.ToString());

      return RestService.For<ITrendyolMarketplaceApi>(httpClient, new RefitSettings
      {
        HttpMessageHandlerFactory = () => new SellerIdInjectingHandler()
      });
      
    }
  }
}