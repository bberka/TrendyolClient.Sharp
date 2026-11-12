using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Replacement or rejected-return package of a claim.</summary>
public class TrendyolIntlClaimPackageInfo
{
    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("cargoTrackingNumber")]
    public string CargoTrackingNumber { get; set; }

    [JsonPropertyName("cargoProviderName")]
    public string CargoProviderName { get; set; }

    [JsonPropertyName("cargoSenderNumber")]
    public string CargoSenderNumber { get; set; }

    [JsonPropertyName("cargoTrackingLink")]
    public string CargoTrackingLink { get; set; }

    /// <summary>Id of the package. For a rejected package, use it with the rejected tracking number update service.</summary>
    [JsonPropertyName("packageId")]
    public long PackageId { get; set; }

    /// <summary>Claim item ids contained in the package.</summary>
    [JsonPropertyName("items")]
    public List<string> Items { get; set; }

    [JsonPropertyName("shipmentAddress")]
    public TrendyolIntlClaimShipmentAddress ShipmentAddress { get; set; }

    /// <summary>True: the rejected package does not have to be shipped back to the customer.</summary>
    [JsonPropertyName("dontShipBack")]
    public bool DontShipBack { get; set; }

    /// <summary>BoxNow compartment PIN for dropping off the rejected package (docs example name).</summary>
    [JsonPropertyName("sellerOtp")]
    public string SellerOtp { get; set; }

    /// <summary>Same value as <see cref="SellerOtp" /> under the name used in the docs prose.</summary>
    [JsonPropertyName("sellerOtpCode")]
    public string SellerOtpCode { get; set; }

    [JsonPropertyName("sellerDeliveryMethod")]
    public string SellerDeliveryMethod { get; set; }
}
