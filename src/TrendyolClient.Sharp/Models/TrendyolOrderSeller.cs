using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolOrderSeller
{
    [JsonPropertyName("sellerId")]
    public long SellerId { get; set; }
}