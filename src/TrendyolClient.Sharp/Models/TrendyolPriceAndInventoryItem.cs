using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolPriceAndInventoryItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Sellable stock, max 20000 per product. May be omitted when only the price changes.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("salePrice")]
    public decimal? SalePrice { get; set; }

    [JsonPropertyName("listPrice")]
    public decimal? ListPrice { get; set; }
}
