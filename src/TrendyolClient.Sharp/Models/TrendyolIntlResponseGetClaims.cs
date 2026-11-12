using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Page of returned orders from the International Marketplace (DE, CEE and GULF share this shape).</summary>
public class TrendyolIntlResponseGetClaims
{
    [JsonPropertyName("content")]
    public List<TrendyolIntlClaim> Content { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("totalElements")]
    public long TotalElements { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}
