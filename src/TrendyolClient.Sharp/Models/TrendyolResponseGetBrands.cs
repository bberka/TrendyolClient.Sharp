using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetBrands
{
    [JsonPropertyName("brands")]
    public List<TrendyolBrand> Brands { get; set; }
}