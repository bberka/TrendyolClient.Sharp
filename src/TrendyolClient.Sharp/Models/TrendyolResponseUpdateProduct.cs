using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseUpdateProduct
{
    [JsonPropertyName("batchRequestId")]
    public string BatchRequestId { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolBatchRequestItem> Items { get; set; }
}