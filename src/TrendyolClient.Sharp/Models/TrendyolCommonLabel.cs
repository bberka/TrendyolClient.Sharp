using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCommonLabel
{
    /// <summary>ZPL content, or a PDF link for product labels.</summary>
    [JsonPropertyName("label")]
    public string Label { get; set; }

    /// <summary>ZPL or PDF.</summary>
    [JsonPropertyName("format")]
    public string Format { get; set; }

    /// <summary>CARGO or PRODUCT (with-product-labels endpoint only).</summary>
    [JsonPropertyName("labelType")]
    public string LabelType { get; set; }

    /// <summary>Product barcode (product labels only).</summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>Set instead of a label when no label could be produced or is not required.</summary>
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
