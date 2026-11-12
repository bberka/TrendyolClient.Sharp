using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>City, district or neighborhood.</summary>
public class TrendyolLocation
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
