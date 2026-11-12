using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Models;
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

    // ---- storefront (International marketplace) ---------------------------------------------------------------

    [Fact]
    public async Task Storefront_code_is_sent_as_a_header_on_every_request_of_the_client()
    {
        var (factory, transport) = Harness.Create();
        var client = factory.GetOrCreateMarketplaceClient(Seller, "k", "s", storeFrontCode: "de");

        await client.GetCountriesAsync();
        await client.GetBrandsAsync(new TrendyolFilterGetBrands());

        Assert.All(transport.Requests, r => Assert.Equal("DE", r.Message.Headers.GetValues("storeFrontCode").Single()));
    }

    [Fact]
    public async Task Turkiye_clients_send_no_storefront_header()
    {
        var (factory, transport) = Harness.Create();

        await factory.GetOrCreateMarketplaceClient(Seller, "k", "s").GetCountriesAsync();

        Assert.False(transport.Requests.Single().Message.Headers.Contains("storeFrontCode"));
    }

    [Fact]
    public void Each_storefront_gets_its_own_client_and_the_same_storefront_is_cached()
    {
        var (factory, _) = Harness.Create();

        var de = factory.GetOrCreateMarketplaceClient(Seller, "k", "s", storeFrontCode: "DE");
        var sa = factory.GetOrCreateMarketplaceClient(Seller, "k", "s", storeFrontCode: "SA");

        Assert.NotSame(de, sa);
        Assert.Same(de, factory.GetOrCreateMarketplaceClient(Seller, "k", "s", storeFrontCode: "de"));
        Assert.NotSame(de, factory.GetOrCreateMarketplaceClient(Seller, "k", "s"));
    }

    [Fact]
    public async Task International_client_sends_the_storefront_header()
    {
        var (factory, transport) = Harness.Create();
        var client = factory.GetOrCreateInternationalClient(Seller, "k", "s", "SA");

        await client.GetClaimsGulfAsync(new TrendyolFilterGetClaims());

        var request = transport.Requests.Single();
        Assert.StartsWith($"/integration/order/sellers/{Seller}/claims-gulf", request.PathAndQuery);
        Assert.Equal("SA", request.Message.Headers.GetValues("storeFrontCode").Single());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void International_client_requires_a_storefront_code(string storeFront)
    {
        var (factory, _) = Harness.Create();

        Assert.Throws<ArgumentException>(() => factory.GetOrCreateInternationalClient(Seller, "k", "s", storeFront));
    }

    [Fact]
    public void Invalidate_drops_every_storefront_client_of_the_seller()
    {
        var (factory, _) = Harness.Create();
        var de = factory.GetOrCreateMarketplaceClient(Seller, "k", "s", storeFrontCode: "DE");
        var tr = factory.GetOrCreateMarketplaceClient(Seller, "k", "s");

        factory.InvalidateClient(Seller);

        Assert.NotSame(de, factory.GetOrCreateMarketplaceClient(Seller, "k", "s", storeFrontCode: "DE"));
        Assert.NotSame(tr, factory.GetOrCreateMarketplaceClient(Seller, "k", "s"));
    }
}
