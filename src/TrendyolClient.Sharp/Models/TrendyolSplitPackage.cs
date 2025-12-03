using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolSplitPackage
{
    [JsonPropertyName("packageDetails")]
    public List<TrendyolSplitPackageDetail> PackageDetails { get; set; }
}