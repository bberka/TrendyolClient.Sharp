using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolOrderPackageLine
{
    [JsonPropertyName("lineId")]
    public long LineId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}