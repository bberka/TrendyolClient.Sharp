using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Tests.Support;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

/// <summary>How a consumer wires the library up: credentials, dependency injection, custom handlers.</summary>
public class SetupTests
{
    private const long Seller = Harness.SellerId;

    // ---- credentials ------------------------------------------------------------------------------------------

    [Fact]
    public void Credentials_and_separate_arguments_share_one_cached_client()
    {
        var (factory, _) = Harness.Create();

        var byArguments = factory.GetOrCreateMarketplaceClient(Seller, "k", "s", true, "de");
        var byCredentials = factory.GetOrCreateMarketplaceClient(new TrendyolCredentials(Seller, "k", "s", true, "DE"));

        Assert.Same(byArguments, byCredentials);
    }

    [Fact]
    public void Credentials_text_never_contains_the_key_or_the_secret()
    {
        var text = new TrendyolCredentials(Seller, "my-api-key", "my-api-secret", false, "DE").ToString();

        Assert.DoesNotContain("my-api-key", text);
        Assert.DoesNotContain("my-api-secret", text);
        Assert.Contains(Seller.ToString(), text);
    }

    [Fact]
    public void Missing_credentials_fail_fast()
    {
        var (factory, _) = Harness.Create();

        Assert.Throws<ArgumentNullException>(() => factory.GetOrCreateMarketplaceClient((TrendyolCredentials)null));
    }

    [Fact]
    public void International_credentials_need_a_storefront()
    {
        var (factory, _) = Harness.Create();

        Assert.Throws<ArgumentException>(() => factory.GetOrCreateInternationalClient(new TrendyolCredentials(Seller, "k", "s")));
    }

    // ---- the seller id never leaves the process -----------------------------------------------------------------

    [Fact]
    public async Task The_internal_seller_header_is_not_sent_to_Trendyol()
    {
        var (factory, transport) = Harness.Create();

        await factory.GetOrCreateMarketplaceClient(Seller, "k", "s").GetCountriesAsync();

        Assert.False(transport.Requests.Single().Message.Headers.Contains("X-Internal-Seller-Id"));
    }

    // ---- middleware through the returned builder ----------------------------------------------------------------

    private sealed class SpyHandler : DelegatingHandler
    {
        public string SellerSeen;
        public bool HeaderSeen;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HeaderSeen = request.Headers.TryGetValues("X-Internal-Seller-Id", out var values);
            SellerSeen = values?.SingleOrDefault();
            return base.SendAsync(request, cancellationToken);
        }
    }

    [Fact]
    public async Task A_handler_added_on_the_returned_builder_runs_and_can_read_the_seller_id()
    {
        var spy = new SpyHandler();
        var (factory, transport) = Harness.Create(customize: builder => builder.AddHttpMessageHandler(() => spy));

        await factory.GetOrCreateMarketplaceClient(Seller, "k", "s").GetCountriesAsync();

        Assert.True(spy.HeaderSeen);
        Assert.Equal(Seller.ToString(), spy.SellerSeen);
        // ...and the header is still gone by the time the request reaches the network
        Assert.False(transport.Requests.Single().Message.Headers.Contains("X-Internal-Seller-Id"));
    }

    [Fact]
    public async Task A_handler_added_on_the_builder_can_short_circuit_a_call()
    {
        var (factory, transport) = Harness.Create(customize: builder => builder.AddHttpMessageHandler(() => new ShortCircuit()));

        var response = await factory.GetOrCreateMarketplaceClient(Seller, "k", "s").GetCountriesAsync();

        Assert.Equal(System.Net.HttpStatusCode.TooManyRequests, response.StatusCode);
        Assert.Empty(transport.Requests);
    }

    private sealed class ShortCircuit : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.TooManyRequests) { RequestMessage = request });
        }
    }

    // ---- single seller registration -----------------------------------------------------------------------------

    [Fact]
    public async Task AddTrendyolSeller_makes_the_clients_injectable()
    {
        var (provider, transport) = Harness.CreateServices(register: s => s.AddTrendyolSeller(new TrendyolCredentials(Seller, "k", "s")));

        var marketplace = provider.GetRequiredService<ITrendyolMarketplaceApi>();
        await marketplace.GetCountriesAsync();

        Assert.Equal($"/integration/member/countries", transport.Requests.Single().PathAndQuery);
        Assert.NotNull(provider.GetRequiredService<ITrendyolWebhookApi>());
        Assert.NotNull(provider.GetRequiredService<ITrendyolFinanceApi>());
        Assert.NotNull(provider.GetRequiredService<ITrendyolExportApi>());
        Assert.NotNull(provider.GetRequiredService<ITrendyolGlobalPartnerApi>());
    }

    [Fact]
    public void Area_interfaces_resolve_to_the_marketplace_client_so_one_area_can_be_faked()
    {
        var (provider, _) = Harness.CreateServices(register: s => s.AddTrendyolSeller(new TrendyolCredentials(Seller, "k", "s")));
        var marketplace = provider.GetRequiredService<ITrendyolMarketplaceApi>();

        Assert.Same(marketplace, provider.GetRequiredService<ITrendyolOrderApi>());
        Assert.Same(marketplace, provider.GetRequiredService<ITrendyolProductApi>());
        Assert.Same(marketplace, provider.GetRequiredService<ITrendyolClaimApi>());
        Assert.Same(marketplace, provider.GetRequiredService<ITrendyolQnAApi>());
    }

    [Fact]
    public async Task AddTrendyolSeller_can_read_the_credentials_when_first_resolved()
    {
        var resolved = 0;
        var (provider, transport) = Harness.CreateServices(register: s => s.AddTrendyolSeller(_ =>
        {
            resolved++;
            return new TrendyolCredentials(Seller, "late-key", "late-secret");
        }));
        Assert.Equal(0, resolved);

        await provider.GetRequiredService<ITrendyolMarketplaceApi>().GetCountriesAsync();

        Assert.True(resolved > 0);
        Assert.Single(transport.Requests);
    }

    [Fact]
    public void The_International_client_resolves_only_with_a_storefront()
    {
        var (withoutStorefront, _) = Harness.CreateServices(register: s => s.AddTrendyolSeller(new TrendyolCredentials(Seller, "k", "s")));
        var (withStorefront, _) = Harness.CreateServices(register: s => s.AddTrendyolSeller(new TrendyolCredentials(Seller, "k", "s", false, "DE")));

        Assert.Throws<ArgumentException>(() => withoutStorefront.GetRequiredService<ITrendyolInternationalApi>());
        Assert.NotNull(withStorefront.GetRequiredService<ITrendyolInternationalApi>());
        Assert.NotNull(withStorefront.GetRequiredService<ITrendyolInternationalClaimApi>());
    }
}
