using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of splitShipmentPackage (International).</summary>
public class TrendyolIntlRequestSplitShipmentPackage
{
    /// <summary>Order line ids to move into the new package.</summary>
    [JsonPropertyName("orderLineIds")]
    public List<long> OrderLineIds { get; set; }

    /// <summary>True keeps the previous package status, false (the default) creates the new package in Created status.</summary>
    [JsonPropertyName("shouldKeepPreviousStatus")]
    public bool? ShouldKeepPreviousStatus { get; set; }
}
