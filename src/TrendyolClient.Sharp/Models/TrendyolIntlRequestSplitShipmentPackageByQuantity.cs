using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of splitShipmentPackageByQuantity (International).</summary>
public class TrendyolIntlRequestSplitShipmentPackageByQuantity
{
    /// <summary>Per order line, the quantities of the new packages.</summary>
    [JsonPropertyName("quantitySplit")]
    public List<TrendyolQuantitySplitItem> QuantitySplit { get; set; }

    /// <summary>True keeps the previous package status, false (the default) creates the new package in Created status.</summary>
    [JsonPropertyName("shouldKeepPreviousStatus")]
    public bool? ShouldKeepPreviousStatus { get; set; }
}
