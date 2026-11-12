using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;
using TrendyolClient.Sharp.Models;
using Xunit;

namespace TrendyolClient.Sharp.Tests;

public class ConverterTests
{
    private sealed class FlexibleHolder
    {
        [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
        public string Value { get; set; }
    }

    private sealed class ListHolder
    {
        [JsonConverter(typeof(TrendyolStringListConverter))]
        public List<string> Values { get; set; }
    }

    private static T Read<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    // ---- TrendyolFlexibleStringConverter ----------------------------------------------------------------------

    [Theory]
    [InlineData("""{"value":"7260242095"}""", "7260242095")]
    [InlineData("""{"value":7260242095}""", "7260242095")]
    // Larger than long.MaxValue: the reason this converter exists (claim cargo tracking numbers)
    [InlineData("""{"value":72602420957047272632}""", "72602420957047272632")]
    [InlineData("""{"value":1.50}""", "1.50")]
    [InlineData("""{"value":true}""", "true")]
    [InlineData("""{"value":false}""", "false")]
    [InlineData("""{"value":""}""", "")]
    [InlineData("""{"value":null}""", null)]
    [InlineData("""{}""", null)]
    public void Flexible_string_reads_strings_and_numbers(string json, string expected)
    {
        Assert.Equal(expected, Read<FlexibleHolder>(json).Value);
    }

    [Fact]
    public void Flexible_string_writes_a_json_string()
    {
        Assert.Equal("""{"Value":"123"}""", JsonSerializer.Serialize(new FlexibleHolder { Value = "123" }));
    }

    // ---- TrendyolStringListConverter --------------------------------------------------------------------------

    [Theory]
    [InlineData("""{"values":["a","b"]}""", new[] { "a", "b" })]
    [InlineData("""{"values":[]}""", new string[0])]
    // Trendyol sends "" instead of [] when there is nothing to report
    [InlineData("""{"values":""}""", new string[0])]
    [InlineData("""{"values":"single"}""", new[] { "single" })]
    public void String_list_accepts_arrays_and_bare_strings(string json, string[] expected)
    {
        Assert.Equal(expected, Read<ListHolder>(json).Values);
    }

    [Fact]
    public void String_list_reads_null_as_null()
    {
        Assert.Null(Read<ListHolder>("""{"values":null}""").Values);
    }

    [Fact]
    public void String_list_round_trips()
    {
        var json = JsonSerializer.Serialize(new ListHolder { Values = ["x", "y"] });
        Assert.Equal("""{"Values":["x","y"]}""", json);
        Assert.Equal(["x", "y"], Read<ListHolder>(json).Values);
    }

    [Fact]
    public void String_list_writes_null_as_null_instead_of_throwing()
    {
        Assert.Equal("""{"Values":null}""", JsonSerializer.Serialize(new ListHolder { Values = null }));
    }

    // ---- Upper snake case enums -------------------------------------------------------------------------------

    [Theory]
    [InlineData(TrendyolOrderStatus.Created, "CREATED")]
    [InlineData(TrendyolOrderStatus.AtCollectionPoint, "AT_COLLECTION_POINT")]
    [InlineData(TrendyolOrderStatus.Undelivered, "UNDELIVERED")]
    [InlineData(TrendyolOrderStatus.Unsupplied, "UNSUPPLIED")]
    public void Order_status_serializes_as_upper_snake_case(TrendyolOrderStatus status, string wire)
    {
        Assert.Equal($"\"{wire}\"", JsonSerializer.Serialize(status));
        Assert.Equal(status, JsonSerializer.Deserialize<TrendyolOrderStatus>($"\"{wire}\""));
    }

    [Fact]
    public void Every_order_status_round_trips()
    {
        foreach (var status in Enum.GetValues<TrendyolOrderStatus>())
            Assert.Equal(status, JsonSerializer.Deserialize<TrendyolOrderStatus>(JsonSerializer.Serialize(status)));
    }

    [Fact]
    public void Order_status_names_match_the_webhook_documentation()
    {
        // https://developers.trendyol.com/docs/webhook-model lists exactly these subscribable statuses
        var documented = new[]
        {
            "CREATED", "PICKING", "INVOICED", "SHIPPED", "CANCELLED", "DELIVERED", "UNDELIVERED", "RETURNED", "UNSUPPLIED", "AWAITING", "UNPACKED",
            "AT_COLLECTION_POINT", "VERIFIED"
        };
        var ours = Enum.GetValues<TrendyolOrderStatus>()
            .Select(s => JsonSerializer.Serialize(s).Trim('"'))
            .ToArray();
        Assert.Equal(documented.OrderBy(x => x), ours.OrderBy(x => x));
    }

    [Theory]
    [InlineData(TrendyolWebhookAuthenticationType.BasicAuthentication, "BASIC_AUTHENTICATION")]
    [InlineData(TrendyolWebhookAuthenticationType.ApiKey, "API_KEY")]
    public void Webhook_authentication_type_uses_the_documented_names(TrendyolWebhookAuthenticationType type, string wire)
    {
        Assert.Equal($"\"{wire}\"", JsonSerializer.Serialize(type));
        Assert.Equal(type, JsonSerializer.Deserialize<TrendyolWebhookAuthenticationType>($"\"{wire}\""));
    }

    [Fact]
    public void Unknown_enum_value_fails_loudly()
    {
        // Documented behavior: a status Trendyol adds later is not silently mapped to a wrong member.
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TrendyolOrderStatus>("\"BRAND_NEW_STATUS\""));
    }

    // ---- Category tree: bare array or { "categories": [...] } -------------------------------------------------

    [Fact]
    public void Category_tree_accepts_a_bare_array_and_the_wrapped_form()
    {
        const string node = """{"id":1,"name":"Root","parentId":null,"subCategories":[{"id":2,"name":"Child","parentId":1,"subCategories":[]}]}""";
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var bare = JsonSerializer.Deserialize<TrendyolResponseGetCategories>($"[{node}]", options);
        var wrapped = JsonSerializer.Deserialize<TrendyolResponseGetCategories>($"{{\"categories\":[{node}]}}", options);

        foreach (var response in new[] { bare, wrapped })
        {
            var root = Assert.Single(response.Categories);
            Assert.Equal("Root", root.Name);
            Assert.Equal("Child", Assert.Single(root.SubCategories).Name);
        }
    }
}
