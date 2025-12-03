using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestApproveClaim
{
    [JsonPropertyName("claimLineItemIdList")]
    public List<string> ClaimLineItemIdList { get; set; }

    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; set; }
}