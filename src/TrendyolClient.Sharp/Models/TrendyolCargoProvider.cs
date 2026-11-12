using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCargoProvider
{
    /// <summary>
    ///     Send in the cargoProviders field of product requests.
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
