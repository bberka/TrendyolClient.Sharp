using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaim
{
    [JsonPropertyName("claimId")]
    public long ClaimId { get; set; }

    [JsonPropertyName("orderShipmentPackageId")]
    public long OrderShipmentPackageId { get; set; }

    [JsonPropertyName("claimStatus")]
    public string ClaimStatus { get; set; }

    [JsonPropertyName("claimType")]
    public string ClaimType { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolClaimItem> Items { get; set; }
}