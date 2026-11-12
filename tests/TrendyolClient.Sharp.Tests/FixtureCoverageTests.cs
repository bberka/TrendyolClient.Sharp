using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using TrendyolClient.Sharp.Models;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

/// <summary>
///     Deserializes the example payloads published in Trendyol's docs into our models and re-serializes them. Any key
///     present in the example but missing from the round trip means the model silently drops that field.
/// </summary>
public class FixtureCoverageTests
{
    private static readonly JsonSerializerOptions Options = Build();

    private static JsonSerializerOptions Build()
    {
        // Exactly the options the clients use, plus tolerance for the comments in the docs' examples.
        var o = TrendyolJson.CreateOptions();
        o.ReadCommentHandling = JsonCommentHandling.Skip;
        return o;
    }

    public static IEnumerable<object[]> Cases()
    {
        yield return ["order-v2.json", typeof(TrendyolResponseGetOrderPackages)];
        yield return ["products-approved.json", typeof(TrendyolResponseGetApprovedProducts)];
        yield return ["product-base.json", typeof(TrendyolResponseGetProductBase)];
        yield return ["products-inventory-price.json", typeof(TrendyolResponseGetApprovedProductsInventoryAndPrice)];
        yield return ["create-product-request.json", typeof(TrendyolRequestCreateProduct)];
        yield return ["webhook-payload.json", typeof(TrendyolWebhookPayload)];
        yield return ["claims.json", typeof(TrendyolResponseGetClaims)];
        yield return ["settlements.json", typeof(TrendyolResponseGetSettlements)];
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public void Model_keeps_every_field_of_the_documented_example(string fixture, Type modelType)
    {
        var json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Fixtures", fixture));

        var model = JsonSerializer.Deserialize(json, modelType, Options);
        Assert.NotNull(model);

        var original = JsonNode.Parse(json);
        var roundTrip = JsonNode.Parse(JsonSerializer.Serialize(model, modelType, Options));

        var missing = new List<string>();
        Compare(original, roundTrip, "$", missing);
        Assert.True(missing.Count == 0, $"{modelType.Name} drops {missing.Count} field(s):{Environment.NewLine}{string.Join(Environment.NewLine, missing.Distinct().Take(60))}");
    }

    [Fact]
    public void Comparer_detects_fields_a_model_does_not_have()
    {
        var original = JsonNode.Parse("""{"a":1,"nested":{"b":2,"c":3},"list":[{"d":4}]}""");
        var roundTrip = JsonNode.Parse("""{"a":1,"nested":{"b":2},"list":[{}]}""");
        var missing = new List<string>();
        Compare(original, roundTrip, "$", missing);
        Assert.Equal(["$.nested.c", "$.list[].d"], missing);
    }

    private static void Compare(JsonNode original, JsonNode actual, string path, List<string> missing)
    {
        switch (original)
        {
            case JsonObject o:
                var a = actual as JsonObject;
                foreach (var (key, value) in o)
                {
                    if (value is null) continue;
                    var other = a?.FirstOrDefault(x => string.Equals(x.Key, key, StringComparison.OrdinalIgnoreCase)).Value;
                    if (other is null) missing.Add($"{path}.{key}");
                    else Compare(value, other, $"{path}.{key}", missing);
                }

                break;
            case JsonArray arr:
                var aa = actual as JsonArray;
                if (aa is null) { missing.Add(path); break; }
                for (var i = 0; i < Math.Min(arr.Count, aa.Count); i++)
                    if (arr[i] != null && aa[i] != null) Compare(arr[i], aa[i], $"{path}[]", missing);
                break;
        }
    }
}
