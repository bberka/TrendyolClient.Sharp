using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlClaimAuditItem
{
    [JsonPropertyName("claimId")]
    public string ClaimId { get; set; }

    [JsonPropertyName("claimItemId")]
    public string ClaimItemId { get; set; }

    /// <summary>Empty for the first status.</summary>
    [JsonPropertyName("previousStatus")]
    public string PreviousStatus { get; set; }

    [JsonPropertyName("newStatus")]
    public string NewStatus { get; set; }

    [JsonPropertyName("userInfoDocument")]
    public TrendyolIntlClaimAuditUserInfo UserInfoDocument { get; set; }

    /// <summary>Timestamp (ms).</summary>
    [JsonPropertyName("date")]
    public long Date { get; set; }
}
