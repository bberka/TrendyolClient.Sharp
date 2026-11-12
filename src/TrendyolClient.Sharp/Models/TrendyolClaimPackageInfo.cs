using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Cargo details of an exchange (replacement) package or of a rejected-return package.</summary>
public class TrendyolClaimPackageInfo
{
    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("cargoTrackingNumber")]
    public string CargoTrackingNumber { get; set; }

    [JsonPropertyName("cargoProviderName")]
    public string CargoProviderName { get; set; }

    [JsonPropertyName("cargoSenderNumber")]
    public string CargoSenderNumber { get; set; }

    /// <summary>Set once the package has left the warehouse.</summary>
    [JsonPropertyName("cargoTrackingLink")]
    public string CargoTrackingLink { get; set; }

    /// <summary>Exchange or rejected-return package id. Use it to report your own cargo code.</summary>
    [JsonPropertyName("packageId")]
    public long PackageId { get; set; }

    /// <summary>Claim item ids contained in the package.</summary>
    [JsonPropertyName("items")]
    public List<string> Items { get; set; }

    /// <summary>Rejected packages only.</summary>
    [JsonPropertyName("shipmentAddress")]
    public TrendyolClaimShipmentAddress ShipmentAddress { get; set; }

    /// <summary>True when the package must not be shipped back to the customer (only return reason 2013).</summary>
    [JsonPropertyName("dontShipBack")]
    public bool DontShipBack { get; set; }

    /// <summary>Always null for the Turkey marketplace.</summary>
    [JsonPropertyName("sellerOtp")]
    public string SellerOtp { get; set; }

    /// <summary>Always null for the Turkey marketplace.</summary>
    [JsonPropertyName("sellerDeliveryMethod")]
    public string SellerDeliveryMethod { get; set; }
}
