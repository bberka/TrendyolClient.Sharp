using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of splitMultiPackagesByQuantity (International).</summary>
public class TrendyolIntlRequestSplitMultiPackageByQuantity
{
    /// <summary>Packages to create; lines left out go into a separate new package.</summary>
    [JsonPropertyName("splitPackages")]
    public List<TrendyolSplitPackage> SplitPackages { get; set; }

    /// <summary>True keeps the previous package status, false (the default) creates the new package in Created status.</summary>
    [JsonPropertyName("shouldKeepPreviousStatus")]
    public bool? ShouldKeepPreviousStatus { get; set; }
}
