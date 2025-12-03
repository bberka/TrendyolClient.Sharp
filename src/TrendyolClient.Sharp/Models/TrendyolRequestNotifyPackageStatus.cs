using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestNotifyPackageStatus
{
    [JsonPropertyName("lines")]
    public List<TrendyolOrderPackageLine> Lines { get; set; }

    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}