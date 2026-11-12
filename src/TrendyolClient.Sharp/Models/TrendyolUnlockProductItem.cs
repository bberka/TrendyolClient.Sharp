using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolUnlockProductItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }
}
