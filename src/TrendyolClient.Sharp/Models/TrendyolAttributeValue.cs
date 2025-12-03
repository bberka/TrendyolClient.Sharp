using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolAttributeValue
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}