using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Category tree. The service documentation shows a bare array while the legacy payload wrapped it in a
///     "categories" property; both forms are accepted.
/// </summary>
[JsonConverter(typeof(TrendyolResponseGetCategoriesConverter))]
public class TrendyolResponseGetCategories
{
    public List<TrendyolCategory> Categories { get; set; }
}

internal sealed class TrendyolResponseGetCategoriesConverter : JsonConverter<TrendyolResponseGetCategories>
{
    public override TrendyolResponseGetCategories Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;
        if (root.ValueKind == JsonValueKind.Object)
        {
            root = root.TryGetProperty("categories", out var wrapped) ? wrapped : default;
        }

        return new TrendyolResponseGetCategories
        {
            Categories = root.ValueKind == JsonValueKind.Array
                ? root.Deserialize<List<TrendyolCategory>>(options)
                : new List<TrendyolCategory>()
        };
    }

    public override void Write(Utf8JsonWriter writer, TrendyolResponseGetCategories value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Categories, options);
    }
}
