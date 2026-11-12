using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolBrand
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    ///     Whether the brand can be used on the LUXE channel.
    /// </summary>
    [JsonPropertyName("luxe")]
    public bool Luxe { get; set; }
}
