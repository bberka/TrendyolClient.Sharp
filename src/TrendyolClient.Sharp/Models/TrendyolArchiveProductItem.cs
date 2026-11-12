using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolArchiveProductItem
{
    /// <summary>
    ///     Max 40 chars.
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     true archives the product, false restores it.
    /// </summary>
    [JsonPropertyName("archived")]
    public bool Archived { get; set; }
}
