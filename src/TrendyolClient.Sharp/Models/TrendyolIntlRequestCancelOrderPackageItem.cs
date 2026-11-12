using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of cancelPackage (International): cancels items of a package. The package is split and a new package id is generated.</summary>
public class TrendyolIntlRequestCancelOrderPackageItem
{
    /// <summary>Lines and quantities to cancel.</summary>
    [JsonPropertyName("lines")]
    public List<TrendyolOrderPackageLine> Lines { get; set; }

    /// <summary>500 stock out, 501 faulty/damaged, 502 wrong price, 503 incorrect image/barcode/quantity, 504 integration error, 505 bulk purchase, 506 force majeure.</summary>
    [JsonPropertyName("reasonId")]
    public int ReasonId { get; set; }

    /// <summary>True keeps the previous package status, false (the default) creates the new package in Created status.</summary>
    [JsonPropertyName("shouldKeepPreviousStatus")]
    public bool? ShouldKeepPreviousStatus { get; set; }
}
