using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCommonLabel
{
    [JsonPropertyName("data")]
    public List<TrendyolCommonLabel> Data { get; set; }
}
