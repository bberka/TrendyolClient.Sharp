using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCountry
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>Country code used by the city endpoints.</summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
