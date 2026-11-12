using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolUpdateAuditExistingAttribute
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("customValue")]
    public string CustomValue { get; set; }

    [JsonPropertyName("attributeId")]
    public long AttributeId { get; set; }

    [JsonPropertyName("attributeName")]
    public string AttributeName { get; set; }

    [JsonPropertyName("slicer")]
    public bool Slicer { get; set; }

    [JsonPropertyName("varianter")]
    public bool Varianter { get; set; }

    [JsonPropertyName("allowCustom")]
    public bool AllowCustom { get; set; }
}
