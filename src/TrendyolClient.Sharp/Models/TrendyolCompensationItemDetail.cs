using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCompensationItemDetail
{
    [JsonPropertyName("itemAmount")]
    public double ItemAmount { get; set; }

    [JsonPropertyName("itemCode")]
    public string ItemCode { get; set; }

    [JsonPropertyName("itemCount")]
    public int ItemCount { get; set; }

    [JsonPropertyName("itemName")]
    public string ItemName { get; set; }
}
