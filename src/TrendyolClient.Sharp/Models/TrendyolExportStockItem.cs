using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportStockItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Stock quantity, 0 or more
    /// </summary>
    [JsonPropertyName("stock")]
    public int Stock { get; set; }
}
