using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolCity
{
    [JsonPropertyName("cityId")]
    public long CityId { get; set; }

    [JsonPropertyName("cityName")]
    public string CityName { get; set; }
}