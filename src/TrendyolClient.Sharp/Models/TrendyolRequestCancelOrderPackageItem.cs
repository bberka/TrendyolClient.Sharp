using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCancelOrderPackageItem
{
    [JsonPropertyName("lines")]
    public List<TrendyolOrderPackageLine> Lines { get; set; }

    [JsonPropertyName("reasonId")]
    public int ReasonId { get; set; }
}