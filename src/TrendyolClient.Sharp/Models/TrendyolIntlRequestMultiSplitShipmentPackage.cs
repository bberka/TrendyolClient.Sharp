using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of multiSplitShipmentPackage (International).</summary>
public class TrendyolIntlRequestMultiSplitShipmentPackage
{
    /// <summary>One group per new package. Do not send every line of the package; the remaining lines get their own package.</summary>
    [JsonPropertyName("splitGroups")]
    public List<TrendyolSplitGroup> SplitGroups { get; set; }

    /// <summary>True keeps the previous package status, false (the default) creates the new package in Created status.</summary>
    [JsonPropertyName("shouldKeepPreviousStatus")]
    public bool? ShouldKeepPreviousStatus { get; set; }
}
