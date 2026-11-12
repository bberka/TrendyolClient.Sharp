using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TrendyolClient.Sharp.Handlers;
using TrendyolClient.Sharp.Tests.Support;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

public class HandlerAndDiTests
{
    private static async Task<SeenRequest> Send(string uri, string sellerHeader = "42", string sellerIdHeader = null)
    {
        var transport = new RecordingHandler();
        var client = new HttpClient(new TrendyolSellerIdInjectorMessageHandler { InnerHandler = transport });
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        if (sellerHeader != null) request.Headers.Add("X-Internal-Seller-Id", sellerHeader);
        if (sellerIdHeader != null) request.Headers.Add("SellerID", sellerIdHeader);
        await client.SendAsync(request);
        return transport.Requests.Single();
    }

    [Fact]
    public async Task Injector_replaces_the_placeholder_in_the_path()
    {
        var seen = await Send("https://apigw.trendyol.com/integration/order/sellers/SELLER_ID_PARAMETER/v2/orders?page=1");

        Assert.Equal("/integration/order/sellers/42/v2/orders?page=1", seen.PathAndQuery);
    }

    [Fact]
    public async Task Injector_replaces_the_placeholder_in_the_SellerID_header_too()
    {
        var seen = await Send("https://apigw.trendyol.com/x/SELLER_ID_PARAMETER", sellerIdHeader: "SELLER_ID_PARAMETER");

        Assert.Equal("42", seen.Message.Headers.GetValues("SellerID").Single());
    }

    [Fact]
    public async Task Injector_leaves_a_real_SellerID_header_alone()
    {
        var seen = await Send("https://apigw.trendyol.com/x/SELLER_ID_PARAMETER", sellerIdHeader: "999");

        Assert.Equal("999", seen.Message.Headers.GetValues("SellerID").Single());
    }

    [Fact]
    public async Task Injector_does_nothing_without_the_internal_header()
    {
        var seen = await Send("https://apigw.trendyol.com/x/SELLER_ID_PARAMETER", sellerHeader: null);

        Assert.Equal("/x/SELLER_ID_PARAMETER", seen.PathAndQuery);
    }

    [Fact]
    public void AddTrendyolApiClient_registers_config_and_factory()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddTrendyolApiClient(c =>
        {
            c.IntegrationName = "Acme";
            c.RequestTimeoutSeconds = 45;
        });
        using var provider = services.BuildServiceProvider();

        var config = provider.GetRequiredService<TrendyolClientConfig>();
        Assert.Equal("Acme", config.IntegrationName);
        Assert.Equal(45, config.RequestTimeoutSeconds);
        Assert.False(config.ThrowOnError);
        Assert.True(config.EnableLogging);
        Assert.Same(provider.GetRequiredService<TrendyolMarketplaceClientFactory>(), provider.GetRequiredService<TrendyolMarketplaceClientFactory>());
    }

    [Fact]
    public void Factory_built_from_DI_applies_the_configured_timeout()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddTrendyolApiClient(c => c.RequestTimeoutSeconds = 45);
        using var provider = services.BuildServiceProvider();

        var http = provider.GetRequiredService<IHttpClientFactory>().CreateClient("TrendyolClient");

        Assert.Equal(45, http.Timeout.TotalSeconds);
    }
}
