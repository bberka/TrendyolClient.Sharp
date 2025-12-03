using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetSettlements
{
    [JsonPropertyName("page")]
    public long Page { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("totalPages")]
    public long TotalPages { get; set; }

    [JsonPropertyName("totalElements")]
    public long TotalElements { get; set; }

    [JsonPropertyName("content")]
    public List<TrendyolSettlementTransactionData> Content { get; set; }
}