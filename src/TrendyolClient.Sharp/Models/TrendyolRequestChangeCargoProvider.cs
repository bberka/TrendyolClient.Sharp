using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestChangeCargoProvider
{
    [JsonPropertyName("cargoProvider")]
    public string CargoProvider { get; set; }
}