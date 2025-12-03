using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetClaimAudit
{
    [JsonPropertyName("auditHistory")]
    public List<TrendyolClaimAuditItem> AuditHistory { get; set; }
}