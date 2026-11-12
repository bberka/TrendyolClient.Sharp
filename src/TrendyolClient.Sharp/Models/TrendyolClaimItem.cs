using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimItem
{
    /// <summary>Claim item id. Use it as claimLineItemIdList in approveClaimLineItems and createClaimIssue.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("orderLineItemId")]
    public long OrderLineItemId { get; set; }

    [JsonPropertyName("customerClaimItemReason")]
    public TrendyolClaimReason CustomerClaimItemReason { get; set; }

    [JsonPropertyName("trendyolClaimItemReason")]
    public TrendyolClaimReason TrendyolClaimItemReason { get; set; }

    [JsonPropertyName("claimItemStatus")]
    public TrendyolClaimItemStatusInfo ClaimItemStatus { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("customerNote")]
    public string CustomerNote { get; set; }

    [JsonPropertyName("resolved")]
    public bool Resolved { get; set; }

    /// <summary>Claims left without action for 48 hours are accepted automatically.</summary>
    [JsonPropertyName("autoAccepted")]
    public bool AutoAccepted { get; set; }

    [JsonPropertyName("acceptedBySeller")]
    public bool AcceptedBySeller { get; set; }
}
