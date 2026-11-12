using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolUpdateAuditChangedAttribute
{
    [JsonPropertyName("attributeId")]
    public long AttributeId { get; set; }

    [JsonPropertyName("attributeName")]
    public string AttributeName { get; set; }

    [JsonPropertyName("attributeValueId")]
    public long? AttributeValueId { get; set; }

    [JsonPropertyName("customAttributeValue")]
    public string CustomAttributeValue { get; set; }

    [JsonPropertyName("mediaUrl")]
    public string MediaUrl { get; set; }

    [JsonPropertyName("isAllowedForUpdate")]
    public bool IsAllowedForUpdate { get; set; }
}
