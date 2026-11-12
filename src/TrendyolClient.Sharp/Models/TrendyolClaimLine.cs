using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>An order line together with its returned items.</summary>
public class TrendyolClaimLine
{
    [JsonPropertyName("orderLine")]
    public TrendyolClaimOrderLine OrderLine { get; set; }

    [JsonPropertyName("claimItems")]
    public List<TrendyolClaimItem> ClaimItems { get; set; }
}
