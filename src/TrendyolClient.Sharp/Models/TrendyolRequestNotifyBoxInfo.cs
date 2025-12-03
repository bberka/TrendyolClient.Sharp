using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestNotifyBoxInfo
{
    [JsonPropertyName("boxQuantity")]
    public int BoxQuantity { get; set; }

    [JsonPropertyName("deci")]
    public decimal Deci { get; set; }
}