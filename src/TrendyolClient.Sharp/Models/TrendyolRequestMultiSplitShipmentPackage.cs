using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestMultiSplitShipmentPackage
{
    [JsonPropertyName("splitGroups")]
    public List<TrendyolSplitGroup> SplitGroups { get; set; }
}