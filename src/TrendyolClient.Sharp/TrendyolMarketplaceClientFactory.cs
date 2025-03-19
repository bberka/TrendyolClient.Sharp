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
    // Store API clients along with credentials for validation
    private readonly ConcurrentDictionary<long, (ITrendyolMarketplaceApi client, string apiKey, string apiSecret)> _clients = new();
    private readonly IHttpClientFactory _httpClientFactory;

    public TrendyolMarketplaceClientFactory(IHttpClientFactory httpClientFactory, TrendyolClientConfig config) {
      Config = config;
      _httpClientFactory = httpClientFactory;
    }

    public TrendyolClientConfig Config { get; }

    public ITrendyolMarketplaceApi GetOrCreateClient(long sellerId,
                                                     string apiKey,
                                                     string apiSecret,
                                                     bool useStageApi = false) {
      if (_clients.TryGetValue(sellerId, out var existingClient)) {
        if (existingClient.apiKey == apiKey && existingClient.apiSecret == apiSecret) {
          return existingClient.client;
        }

        InvalidateClient(sellerId);
      }

      return _clients.GetOrAdd(sellerId, _ => {
        var newClient = CreateClient(sellerId, apiKey, apiSecret, useStageApi);
        return (newClient, apiKey, apiSecret); // Store client with credentials
      }).client;
    }

    public void InvalidateClient(long sellerId) {
      _clients.TryRemove(sellerId, out _);
    }

    protected ITrendyolMarketplaceApi CreateClient(long sellerId,
                                                   string apiKey,
                                                   string apiSecret,
                                                   bool useStageApi = false) {
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
      httpClient.DefaultRequestHeaders.Add("User-Agent", $"{sellerId} - {Config.IntegrationName}");

      httpClient.DefaultRequestHeaders.Add("X-Internal-Seller-Id", sellerId.ToString());

      return RestService.For<ITrendyolMarketplaceApi>(httpClient, new RefitSettings {
        UrlParameterKeyFormatter = new LowerCaseFirstCharParameterFormatter()
      });
    }
  }
}