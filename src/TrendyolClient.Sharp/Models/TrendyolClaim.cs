using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaim
{
    /// <summary>Deprecated alias of <see cref="ClaimId" />. The docs examples still return it alongside claimId.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("claimId")]
    public string ClaimId { get; set; }

    /// <summary>1 for the Turkey core channel, 25 for luxury.</summary>
    [JsonPropertyName("channelId")]
    public int ChannelId { get; set; }

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

    [JsonPropertyName("cargoSenderNumber")]
    public string CargoSenderNumber { get; set; }

    [JsonPropertyName("cargoProviderName")]
    public string CargoProviderName { get; set; }

    /// <summary>Id of the returned package.</summary>
    [JsonPropertyName("orderShipmentPackageId")]
    public long OrderShipmentPackageId { get; set; }

    [JsonPropertyName("orderOutboundPackageId")]
    public long OrderOutboundPackageId { get; set; }

    /// <summary>Exchange package details, empty when there is no exchange.</summary>
    [JsonPropertyName("replacementOutboundPackageInfo")]
    public TrendyolClaimPackageInfo ReplacementOutboundPackageInfo { get; set; }

    /// <summary>Package created to ship a rejected return back to the customer. Absent when no such package exists.</summary>
    [JsonPropertyName("rejectedPackageInfo")]
    public TrendyolClaimPackageInfo RejectedPackageInfo { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolClaimLine> Items { get; set; }

    /// <summary>Timestamp (ms). Claims are returned ordered by this value.</summary>
    [JsonPropertyName("lastModifiedDate")]
    public long LastModifiedDate { get; set; }

    /// <summary>Always null for the Turkey marketplace.</summary>
    [JsonPropertyName("sellerOtp")]
    public string SellerOtp { get; set; }

    /// <summary>Always null for the Turkey marketplace.</summary>
    [JsonPropertyName("sellerDeliveryMethod")]
    public string SellerDeliveryMethod { get; set; }

    /// <summary>Always null for the Turkey marketplace.</summary>
    [JsonPropertyName("sellerCollectionPointDetails")]
    public TrendyolClaimCollectionPointDetails SellerCollectionPointDetails { get; set; }
}
