using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolAttributeValue
{
    [JsonPropertyName("attributeValueId")]
    public long AttributeValueId { get; set; }

    /// <summary>
    ///     Value name as defined by the OpenAPI reference.
    /// </summary>
    [JsonPropertyName("attributeValueName")]
    public string AttributeValueName { get; set; }

    /// <summary>
    ///     Value name as shown in the documentation example; only one of the two is populated.
    /// </summary>
    [JsonPropertyName("attributeValue")]
    public string AttributeValue { get; set; }
}
