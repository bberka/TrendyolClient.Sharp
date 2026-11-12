using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportResponseBatch
{
    /// <summary>
    ///     Batch ID to query with the batch status service (kept for 24 hours)
    /// </summary>
    [JsonPropertyName("batchId")]
    public string BatchId { get; set; }
}
