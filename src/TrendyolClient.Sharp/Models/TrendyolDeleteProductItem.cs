using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolDeleteProductItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }
}