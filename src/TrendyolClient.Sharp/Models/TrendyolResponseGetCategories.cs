using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCategories
{
    [JsonPropertyName("categories")]
    public List<TrendyolCategory> Categories { get; set; }
}