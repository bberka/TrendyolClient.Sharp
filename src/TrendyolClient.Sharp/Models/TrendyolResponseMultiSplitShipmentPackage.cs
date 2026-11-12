using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseMultiSplitShipmentPackage
{
    [JsonPropertyName("results")]
    public List<TrendyolMultiSplitResult> Results { get; set; }
}
