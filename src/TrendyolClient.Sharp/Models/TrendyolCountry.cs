using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCountry
{
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("countryName")]
    public string CountryName { get; set; }
}