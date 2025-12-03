using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCategoryAttribute
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("allowCustom")]
    public bool AllowCustom { get; set; }

    [JsonPropertyName("attributeValues")]
    public List<TrendyolAttributeValue> AttributeValues { get; set; }
}