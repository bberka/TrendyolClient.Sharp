using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of createClaimAsAccepted (DE and CEE): creates an already accepted return.</summary>
public class TrendyolIntlRequestCreateClaimAsAccepted
{
    [JsonPropertyName("orderParentNumber")]
    public string OrderParentNumber { get; set; }

    [JsonPropertyName("claimItems")]
    public List<TrendyolIntlRequestCreateClaimAsAcceptedItem> ClaimItems { get; set; }
}
