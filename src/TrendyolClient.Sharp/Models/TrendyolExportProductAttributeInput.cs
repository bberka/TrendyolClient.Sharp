using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportProductAttributeInput
{
    [JsonPropertyName("attributeId")]
    public long AttributeId { get; set; }

    /// <summary>
    ///     Used when allowCustom=false and multipleValues=false
    /// </summary>
    [JsonPropertyName("attributeValueId")]
    public long? AttributeValueId { get; set; }

    /// <summary>
    ///     Used when allowCustom=true and multipleValues=false
    /// </summary>
    [JsonPropertyName("customAttributeValue")]
    public string CustomAttributeValue { get; set; }

    /// <summary>
    ///     Used when allowCustom=true and multipleValues=true (e.g. material composition: composition ID to percentage)
    /// </summary>
    [JsonPropertyName("customAttributeValues")]
    public Dictionary<string, string> CustomAttributeValues { get; set; }
}
