using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportProductAttribute
{
    [JsonPropertyName("attributeId")]
    public long AttributeId { get; set; }

    [JsonPropertyName("attributeName")]
    public string AttributeName { get; set; }

    [JsonPropertyName("valueId")]
    public long? ValueId { get; set; }

    [JsonPropertyName("valueName")]
    public string ValueName { get; set; }
}
