using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolUpdateAuditItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("contentId")]
    public long ContentId { get; set; }

    [JsonPropertyName("batchRequestId")]
    public string BatchRequestId { get; set; }

    [JsonPropertyName("requestDate")]
    public string RequestDate { get; set; }

    [JsonPropertyName("updates")]
    public List<TrendyolUpdateAuditDetail> Updates { get; set; }
}
