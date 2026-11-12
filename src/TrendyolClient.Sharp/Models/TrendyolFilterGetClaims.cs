using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models;

public class TrendyolFilterGetClaims
{
    /// <summary>Comma-separated claim ids. When set, every other filter is ignored.</summary>
    [JsonPropertyName("claimIds")]
    public string ClaimIds { get; set; }

    /// <summary>Status of the claim item. Use Created to fetch new claims.</summary>
    [JsonPropertyName("claimItemStatus")]
    public TrendyolClaimItemStatus? ClaimItemStatus { get; set; }

    /// <summary>Timestamp (ms). Applies to the claim lastModifiedDate.</summary>
    [JsonPropertyName("startDate")]
    public long? StartDate { get; set; }

    /// <summary>Timestamp (ms). Applies to the claim lastModifiedDate.</summary>
    [JsonPropertyName("endDate")]
    public long? EndDate { get; set; }

    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>Zero-based page index.</summary>
    [JsonPropertyName("page")]
    public int? Page { get; set; }

    /// <summary>Page size (default 50, max 200).</summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }
}
