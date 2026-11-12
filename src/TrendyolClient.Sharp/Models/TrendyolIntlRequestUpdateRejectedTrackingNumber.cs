using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of the rejected package tracking number update (DE/CEE and GULF).</summary>
public class TrendyolIntlRequestUpdateRejectedTrackingNumber
{
    /// <summary>Tracking number of the package you ship back.</summary>
    [JsonPropertyName("cargoSenderNumber")]
    public string CargoSenderNumber { get; set; }

    /// <summary>Cargo provider code.</summary>
    [JsonPropertyName("providerCode")]
    public string ProviderCode { get; set; }
}
