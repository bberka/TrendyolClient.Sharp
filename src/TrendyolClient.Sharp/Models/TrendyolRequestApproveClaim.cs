using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestApproveClaim
{
    /// <summary>Claim item ids to approve (WaitingInAction items only).</summary>
    [JsonPropertyName("claimLineItemIdList")]
    public List<string> ClaimLineItemIdList { get; set; }

    [JsonPropertyName("params")]
    public Dictionary<string, string> Params { get; set; }
}
