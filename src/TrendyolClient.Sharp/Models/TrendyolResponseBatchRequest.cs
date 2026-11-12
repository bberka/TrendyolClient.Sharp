using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Response of every queued product operation.
/// </summary>
public class TrendyolResponseBatchRequest
{
    /// <summary>
    ///     Pass to getBatchRequestResult to follow the asynchronous operation.
    /// </summary>
    [JsonPropertyName("batchRequestId")]
    public string BatchRequestId { get; set; }
}
