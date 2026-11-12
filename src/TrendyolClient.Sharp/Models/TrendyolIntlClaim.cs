using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>A returned order (claim) of the International Marketplace.</summary>
public class TrendyolIntlClaim
{
    /// <summary>Same value as <see cref="ClaimId" />.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("claimId")]
    public string ClaimId { get; set; }

    /// <summary>Only present in the documented examples, not in the OpenAPI spec.</summary>
    [JsonPropertyName("channelId")]
    public int? ChannelId { get; set; }

    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>Timestamp (ms).</summary>
    [JsonPropertyName("orderDate")]
    public long OrderDate { get; set; }

    [JsonPropertyName("customerFirstName")]
    public string CustomerFirstName { get; set; }

    [JsonPropertyName("customerLastName")]
    public string CustomerLastName { get; set; }

    /// <summary>Timestamp (ms, GMT). Add 3 hours to match the seller panel (GMT+3).</summary>
    [JsonPropertyName("claimDate")]
    public long ClaimDate { get; set; }

    [JsonConverter(typeof(TrendyolFlexibleStringConverter))]
    [JsonPropertyName("cargoTrackingNumber")]
    public string CargoTrackingNumber { get; set; }

    [JsonPropertyName("cargoTrackingLink")]
    public string CargoTrackingLink { get; set; }

    /// <summary>Carries the tracking number when the carrier generates one that is not purely numeric.</summary>
    [JsonPropertyName("cargoSenderNumber")]
    public string CargoSenderNumber { get; set; }

    [JsonPropertyName("cargoProviderName")]
    public string CargoProviderName { get; set; }

    /// <summary>Id of the returned package.</summary>
    [JsonPropertyName("orderShipmentPackageId")]
    public long OrderShipmentPackageId { get; set; }

    [JsonPropertyName("orderOutboundPackageId")]
    public long OrderOutboundPackageId { get; set; }

    /// <summary>Replacement package (GULF). Empty when there is no replacement.</summary>
    [JsonPropertyName("replacementOutboundPackageInfo")]
    public TrendyolIntlClaimPackageInfo ReplacementOutboundPackageInfo { get; set; }

    /// <summary>Package created to ship a rejected return back to the customer. Absent when none exists.</summary>
    [JsonPropertyName("rejectedPackageInfo")]
    public TrendyolIntlClaimPackageInfo RejectedPackageInfo { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolIntlClaimLine> Items { get; set; }

    /// <summary>Timestamp (ms). Claims are returned ordered by this value.</summary>
    [JsonPropertyName("lastModifiedDate")]
    public long LastModifiedDate { get; set; }

    /// <summary>BoxNow locker PIN to pick up the return (docs example name). Set when <see cref="SellerDeliveryMethod" /> is LOCKER and the item is WaitingInAction.</summary>
    [JsonPropertyName("sellerOtp")]
    public string SellerOtp { get; set; }

    /// <summary>Same value as <see cref="SellerOtp" /> under the name used in the docs prose and changelog.</summary>
    [JsonPropertyName("sellerOtpCode")]
    public string SellerOtpCode { get; set; }

    /// <summary>LOCKER (BoxNow), WAREHOUSE or null.</summary>
    [JsonPropertyName("sellerDeliveryMethod")]
    public string SellerDeliveryMethod { get; set; }

    /// <summary>BoxNow locker to collect the return from. Set when the item is WaitingInAction.</summary>
    [JsonPropertyName("sellerCollectionPointDetails")]
    public TrendyolClaimCollectionPointDetails SellerCollectionPointDetails { get; set; }
}
