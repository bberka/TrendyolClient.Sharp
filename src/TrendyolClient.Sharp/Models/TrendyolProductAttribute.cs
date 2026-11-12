using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Attribute sent when creating or updating products.
/// </summary>
public class TrendyolProductAttribute
{
    [JsonPropertyName("attributeId")]
    public long AttributeId { get; set; }

    /// <summary>
    ///     Value id from getCategoryAttributeValues. Leave null for attributes that allow custom values.
    /// </summary>
    [JsonPropertyName("attributeValueId")]
    public long? AttributeValueId { get; set; }

    /// <summary>
    ///     Multiple value ids; only for attributes whose allowMultipleAttributeValues is true.
    /// </summary>
    [JsonPropertyName("attributeValueIds")]
    public List<long> AttributeValueIds { get; set; }

    /// <summary>
    ///     Free-text value, only for attributes whose allowCustom is true.
    /// </summary>
    [JsonPropertyName("customAttributeValue")]
    public string CustomAttributeValue { get; set; }
}
