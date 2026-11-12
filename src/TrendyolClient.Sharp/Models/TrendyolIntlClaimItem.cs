using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlClaimItem
{
    /// <summary>Claim item id, used in claimLineItemIdList / claimItemIdList.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("orderLineItemId")]
    public long OrderLineItemId { get; set; }

    /// <summary>Return reason chosen by the customer (see the return reasons table of the region docs).</summary>
    [JsonPropertyName("customerClaimItemReason")]
    public TrendyolClaimReason CustomerClaimItemReason { get; set; }

    [JsonPropertyName("trendyolClaimItemReason")]
    public TrendyolClaimReason TrendyolClaimItemReason { get; set; }

    /// <summary>Created, WaitingInAction, Accepted, Cancelled, Rejected, Unresolved or InAnalysis.</summary>
    [JsonPropertyName("claimItemStatus")]
    public TrendyolClaimItemStatusInfo ClaimItemStatus { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("customerNote")]
    public string CustomerNote { get; set; }

    [JsonPropertyName("resolved")]
    public bool Resolved { get; set; }

    /// <summary>True when the item waited 48 hours in WaitingInAction and the system accepted it.</summary>
    [JsonPropertyName("autoAccepted")]
    public bool AutoAccepted { get; set; }

    [JsonPropertyName("acceptedBySeller")]
    public bool AcceptedBySeller { get; set; }

    /// <summary>SUPPLIER, DISPUTE or SYSTEM.</summary>
    [JsonPropertyName("acceptDetail")]
    public string AcceptDetail { get; set; }
}
