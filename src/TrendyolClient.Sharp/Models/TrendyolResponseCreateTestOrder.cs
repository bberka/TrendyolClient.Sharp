using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseCreateTestOrder
{
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }
}