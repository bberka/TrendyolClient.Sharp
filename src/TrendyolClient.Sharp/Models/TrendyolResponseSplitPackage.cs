using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseSplitPackage
{
    [JsonPropertyName("newPackageIds")]
    public List<long> NewPackageIds { get; set; }
}