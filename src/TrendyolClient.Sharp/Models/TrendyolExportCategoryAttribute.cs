using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportCategoryAttribute
{
    [JsonPropertyName("attributeId")]
    public long AttributeId { get; set; }

    [JsonPropertyName("attributeName")]
    public string AttributeName { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("allowCustom")]
    public bool AllowCustom { get; set; }

    /// <summary>
    ///     Alternative flag for allowCustom
    /// </summary>
    [JsonPropertyName("customValue")]
    public bool CustomValue { get; set; }

    [JsonPropertyName("multipleValues")]
    public bool MultipleValues { get; set; }

    [JsonPropertyName("attributeValues")]
    public List<TrendyolExportCategoryAttributeValue> AttributeValues { get; set; }
}
