using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetClaimIssueReasons
{
    [JsonPropertyName("claimIssueReasons")]
    public List<TrendyolClaimIssueReason> ClaimIssueReasons { get; set; }
}