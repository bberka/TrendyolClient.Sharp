using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolPriceAndInventoryItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("salePrice")]
    public double SalePrice { get; set; }

    [JsonPropertyName("listPrice")]
    public double ListPrice { get; set; }
}