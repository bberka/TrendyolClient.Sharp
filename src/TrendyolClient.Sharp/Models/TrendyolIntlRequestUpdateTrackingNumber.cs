using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of updateTrackingNumber (International): notifies the tracking number of a Seller Pays package.</summary>
public class TrendyolIntlRequestUpdateTrackingNumber
{
    /// <summary>Tracking number of the shipment.</summary>
    [JsonPropertyName("cargoSenderNumber")]
    public string CargoSenderNumber { get; set; }

    /// <summary>Cargo provider code, for example DHLMP (see the carrier company lists per region).</summary>
    [JsonPropertyName("providerCode")]
    public string ProviderCode { get; set; }

    /// <summary>Return tracking number. Optional, Germany only.</summary>
    [JsonPropertyName("returnTrackingNumber")]
    public string ReturnTrackingNumber { get; set; }
}
