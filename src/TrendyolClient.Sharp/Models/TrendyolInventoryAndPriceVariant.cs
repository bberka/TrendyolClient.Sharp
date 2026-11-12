using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolInventoryAndPriceVariant
{
    [JsonPropertyName("variantId")]
    public long VariantId { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("salePrice")]
    public decimal SalePrice { get; set; }

    [JsonPropertyName("listPrice")]
    public decimal ListPrice { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("stockCode")]
    public string StockCode { get; set; }

    /// <summary>
    ///     Null when the stock was never updated.
    /// </summary>
    [JsonPropertyName("stockLastModifiedDate")]
    public long? StockLastModifiedDate { get; set; }
}
