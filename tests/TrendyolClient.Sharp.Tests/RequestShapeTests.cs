using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TrendyolClient.Sharp.Enums;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Tests.Support;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

/// <summary>Checks the bytes we put on the wire: query strings, JSON bodies, headers.</summary>
public class RequestShapeTests
{
    private static (ITrendyolMarketplaceApi Client, RecordingHandler Transport) Marketplace()
    {
        var (factory, transport) = Harness.Create();
        return (factory.GetOrCreateMarketplaceClient(Harness.SellerId, "key", "secret"), transport);
    }

    [Fact]
    public async Task Order_filter_becomes_lower_camel_case_query_keys_on_the_v2_route()
    {
        var (client, transport) = Marketplace();

        await client.GetOrderPackagesAsync(new TrendyolFilterGetOrderPackages
        {
            Status = "Created",
            StartDate = 1_700_000_000_000,
            EndDate = 1_700_086_400_000,
            OrderByField = "PackageLastModifiedDate",
            OrderByDirection = "DESC",
            Page = 2,
            Size = 50
        });

        var request = Assert.Single(transport.Requests);
        Assert.Equal("GET", request.Method.Method);
        Assert.StartsWith($"/integration/order/sellers/{Harness.SellerId}/v2/orders?", request.PathAndQuery);
        var query = ParseQuery(request.PathAndQuery);
        Assert.Equal("Created", query["status"]);
        Assert.Equal("1700000000000", query["startDate"]);
        Assert.Equal("1700086400000", query["endDate"]);
        Assert.Equal("PackageLastModifiedDate", query["orderByField"]);
        Assert.Equal("DESC", query["orderByDirection"]);
        Assert.Equal("2", query["page"]);
        Assert.Equal("50", query["size"]);
    }

    [Fact]
    public async Task Unset_filter_properties_are_not_sent()
    {
        var (client, transport) = Marketplace();

        await client.GetOrderPackagesAsync(new TrendyolFilterGetOrderPackages { Status = "Shipped" });

        var query = ParseQuery(transport.Requests.Single().PathAndQuery);
        Assert.Equal(["status"], query.Keys);
    }

    [Fact]
    public async Task Settlement_filter_sends_enum_names_verbatim()
    {
        var (factory, transport) = Harness.Create();
        var finance = factory.GetOrCreateFinanceClient(Harness.SellerId, "key", "secret");

        await finance.GetSettlementsAsync(new TrendyolFilterGetSettlements
        {
            TransactionType = TrendyolSettlementsTransactionType.Sale,
            StartDate = 1,
            EndDate = 2,
            Page = 0,
            Size = 500
        });

        var request = Assert.Single(transport.Requests);
        Assert.StartsWith($"/integration/finance/che/sellers/{Harness.SellerId}/settlements?", request.PathAndQuery);
        var query = ParseQuery(request.PathAndQuery);
        Assert.Equal("Sale", query["transactionType"]);
        Assert.Equal("500", query["size"]);
    }

    [Fact]
    public async Task Price_update_omits_fields_that_were_not_set()
    {
        var (client, transport) = Marketplace();

        await client.UpdatePriceAndInventoryAsync(new TrendyolRequestUpdatePriceAndInventory
        {
            Items = [new TrendyolPriceAndInventoryItem { Barcode = "B1", SalePrice = 99.9m, ListPrice = 120m }]
        });

        var request = Assert.Single(transport.Requests);
        Assert.Equal($"/integration/inventory/sellers/{Harness.SellerId}/products/price-and-inventory", request.PathAndQuery);
        var item = JsonNode.Parse(request.Body)["items"][0].AsObject();
        Assert.Equal("B1", (string)item["barcode"]);
        Assert.Equal(99.9m, (decimal)item["salePrice"]);
        Assert.Equal(120m, (decimal)item["listPrice"]);
        // quantity was not set: a partial price update must not zero the stock
        Assert.False(item.ContainsKey("quantity"), "quantity must be omitted, not sent as null/0");
    }

    [Fact]
    public async Task Webhook_creation_uses_the_documented_enum_names()
    {
        var (factory, transport) = Harness.Create();
        var webhook = factory.GetOrCreateWebhookClient(Harness.SellerId, "key", "secret");

        await webhook.CreateWebhookAsync(new TrendyolRequestCreateWebhook
        {
            Url = "https://example.com/hook",
            Username = "u",
            Password = "p",
            AuthenticationType = TrendyolWebhookAuthenticationType.BasicAuthentication,
            SubscribedStatuses = [TrendyolOrderStatus.Created, TrendyolOrderStatus.AtCollectionPoint]
        });

        var body = JsonNode.Parse(transport.Requests.Single().Body).AsObject();
        Assert.Equal("BASIC_AUTHENTICATION", (string)body["authenticationType"]);
        Assert.Equal(["CREATED", "AT_COLLECTION_POINT"], body["subscribedStatuses"].AsArray().Select(x => (string)x));
    }

    [Fact]
    public async Task Test_order_endpoints_receive_the_seller_id_header()
    {
        var (client, transport) = Marketplace();

        await client.CreateTestOrderAsync(new TrendyolRequestCreateTestOrder());

        var request = Assert.Single(transport.Requests);
        Assert.Equal(Harness.SellerId.ToString(), request.Message.Headers.GetValues("SellerID").Single());
    }

    private static Dictionary<string, string> ParseQuery(string pathAndQuery)
    {
        var query = pathAndQuery[(pathAndQuery.IndexOf('?') + 1)..];
        return query.Split('&', System.StringSplitOptions.RemoveEmptyEntries)
            .Select(pair => pair.Split('=', 2))
            .ToDictionary(p => System.Uri.UnescapeDataString(p[0]), p => System.Uri.UnescapeDataString(p.ElementAtOrDefault(1) ?? ""));
    }
}
