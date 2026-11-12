using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlClaimLine
{
    [JsonPropertyName("orderLine")]
    public TrendyolIntlClaimOrderLine OrderLine { get; set; }

    [JsonPropertyName("claimItems")]
    public List<TrendyolIntlClaimItem> ClaimItems { get; set; }
}
