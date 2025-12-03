using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseDeleteProduct
{
    [JsonPropertyName("batchRequestId")]
    public string BatchRequestId { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolBatchRequestItem> Items { get; set; }
}