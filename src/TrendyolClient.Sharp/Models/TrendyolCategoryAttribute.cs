using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCategoryAttribute
{
    [JsonPropertyName("attribute")]
    public TrendyolAttribute Attribute { get; set; }

    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }

    /// <summary>
    ///     If true, send customAttributeValue instead of a value id.
    /// </summary>
    [JsonPropertyName("allowCustom")]
    public bool AllowCustom { get; set; }

    /// <summary>
    ///     The attribute is a variant (e.g. size) within one content.
    /// </summary>
    [JsonPropertyName("varianter")]
    public bool Varianter { get; set; }

    /// <summary>
    ///     The attribute opens a separate content (e.g. color).
    /// </summary>
    [JsonPropertyName("slicer")]
    public bool Slicer { get; set; }

    [JsonPropertyName("allowMultipleAttributeValues")]
    public bool AllowMultipleAttributeValues { get; set; }
}
