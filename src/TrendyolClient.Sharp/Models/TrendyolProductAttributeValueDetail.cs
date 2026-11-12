using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolProductAttributeValueDetail
{
    [JsonPropertyName("attributeValueId")]
    public long? AttributeValueId { get; set; }

    [JsonPropertyName("attributeValue")]
    public string AttributeValue { get; set; }
}
