using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Attribute as returned by the product filter services.
/// </summary>
public class TrendyolProductAttributeDetail
{
    [JsonPropertyName("attributeId")]
    public long AttributeId { get; set; }

    [JsonPropertyName("attributeName")]
    public string AttributeName { get; set; }

    [JsonPropertyName("attributeValueId")]
    public long? AttributeValueId { get; set; }

    [JsonPropertyName("attributeValue")]
    public string AttributeValue { get; set; }

    /// <summary>
    ///     Multi-value form returned by the OpenAPI reference.
    /// </summary>
    [JsonPropertyName("attributeValues")]
    public List<TrendyolProductAttributeValueDetail> AttributeValues { get; set; }
}
