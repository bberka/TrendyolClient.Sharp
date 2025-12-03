using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCountries
{
    [JsonPropertyName("countries")]
    public List<TrendyolCountry> Countries { get; set; }
}