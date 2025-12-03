using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestSplitMultiPackageByQuantity
{
    [JsonPropertyName("splitPackages")]
    public List<TrendyolSplitPackage> SplitPackages { get; set; }
}