using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlResponseCreateClaimAsAccepted
{
    [JsonPropertyName("claimId")]
    public string ClaimId { get; set; }

    [JsonPropertyName("claimItemIds")]
    public List<string> ClaimItemIds { get; set; }
}
