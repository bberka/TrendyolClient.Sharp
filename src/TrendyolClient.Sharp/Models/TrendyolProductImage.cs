using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolProductImage
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}