using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Tests.Support;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

public class FactoryTests
{
    private const long Seller = Harness.SellerId;

    [Fact]
    public void Same_seller_and_credentials_reuse_the_cached_client()
    {
        var (factory, _) = Harness.Create();

        var a = factory.GetOrCreateMarketplaceClient(Seller, "k", "s");
        var b = factory.GetOrCreateMarketplaceClient(Seller, "k", "s");

        Assert.Same(a, b);
    }

    [Fact]
    public void Different_sellers_get_different_clients()
    {
        var (factory, _) = Harness.Create();

        Assert.NotSame(factory.GetOrCreateMarketplaceClient(1, "k", "s"), factory.GetOrCreateMarketplaceClient(2, "k", "s"));
    }

    [Fact]
    public void Changed_credentials_rebuild_the_client()
    {
        var (factory, _) = Harness.Create();

        var before = factory.GetOrCreateMarketplaceClient(Seller, "k", "old-secret");
        var after = factory.GetOrCreateMarketplaceClient(Seller, "k", "new-secret");

        Assert.NotSame(before, after);
    }

    [Fact]
    public void InvalidateClient_forces_a_new_client()
    {
        var (factory, _) = Harness.Create();
        var before = factory.GetOrCreateMarketplaceClient(Seller, "k", "s");

        factory.InvalidateClient(Seller);

        Assert.NotSame(before, factory.GetOrCreateMarketplaceClient(Seller, "k", "s"));
    }

    [Fact]
    public void Each_api_family_is_cached_per_seller()
    {
        var (factory, _) = Harness.Create();

        Assert.Same(factory.GetOrCreateFinanceClient(Seller, "k", "s"), factory.GetOrCreateFinanceClient(Seller, "k", "s"));
        Assert.Same(factory.GetOrCreateWebhookClient(Seller, "k", "s"), factory.GetOrCreateWebhookClient(Seller, "k", "s"));
        Assert.Same(factory.GetOrCreateExportClient(Seller, "k", "s"), factory.GetOrCreateExportClient(Seller, "k", "s"));
    }

    [Fact]
    public async Task Production_and_stage_clients_for_one_seller_hit_their_own_hosts()
    {
        var (factory, transport) = Harness.Create();
        var prod = factory.GetOrCreateMarketplaceClient(Seller, "k", "s");
        var stage = factory.GetOrCreateMarketplaceClient(Seller, "k", "s", true);

        await prod.GetCountriesAsync();
        await stage.GetCountriesAsync();

        Assert.Equal(["apigw.trendyol.com", "stageapigw.trendyol.com"], transport.Requests.Select(r => r.Host));
    }

    [Fact]
    public async Task Invalidate_drops_both_environments()
    {
        var (factory, _) = Harness.Create();
        var prod = factory.GetOrCreateMarketplaceClient(Seller, "k", "s");
        var stage = factory.GetOrCreateMarketplaceClient(Seller, "k", "s", true);

        factory.InvalidateClient(Seller);

        Assert.NotSame(prod, factory.GetOrCreateMarketplaceClient(Seller, "k", "s"));
        Assert.NotSame(stage, factory.GetOrCreateMarketplaceClient(Seller, "k", "s", true));
    }

    [Fact]
    public async Task Requests_carry_the_headers_Trendyol_requires()
    {
        var (factory, transport) = Harness.Create(new TrendyolClientConfig { IntegrationName = "AcmeSoft" });
        var client = factory.GetOrCreateMarketplaceClient(Seller, "my-key", "my-secret");

        await client.GetCountriesAsync();

        var headers = transport.Requests.Single().Message.Headers;
        var expectedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes("my-key:my-secret"));
        Assert.Equal("Basic", headers.Authorization.Scheme);
        Assert.Equal(expectedAuth, headers.Authorization.Parameter);
        // docs: "{sellerId} - {integration name}"; requests without a User-Agent get a 403
        Assert.Equal($"{Seller} - AcmeSoft", headers.UserAgent.ToString());
        Assert.Contains(headers.AcceptEncoding, e => e.Value == "gzip");
    }

    [Fact]
    public async Task Default_integration_name_is_SelfIntegration()
    {
        var (factory, transport) = Harness.Create();

        await factory.GetOrCreateMarketplaceClient(Seller, "k", "s").GetCountriesAsync();

        Assert.Equal($"{Seller} - SelfIntegration", transport.Requests.Single().Message.Headers.UserAgent.ToString());
    }
}
