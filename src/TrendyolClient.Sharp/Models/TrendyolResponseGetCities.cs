using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCities
{
    [JsonPropertyName("cities")]
    public List<TrendyolCity> Cities { get; set; }
}