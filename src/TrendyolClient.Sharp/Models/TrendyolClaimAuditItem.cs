using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimAuditItem
{
    [JsonPropertyName("claimId")]
    public string ClaimId { get; set; }

    [JsonPropertyName("claimItemId")]
    public string ClaimItemId { get; set; }

    [JsonPropertyName("previousStatus")]
    public string PreviousStatus { get; set; }

    [JsonPropertyName("newStatus")]
    public string NewStatus { get; set; }

    [JsonPropertyName("userInfoDocument")]
    public TrendyolClaimAuditUserInfo UserInfoDocument { get; set; }

    /// <summary>Timestamp (ms) of the status change.</summary>
    [JsonPropertyName("date")]
    public long Date { get; set; }
}
