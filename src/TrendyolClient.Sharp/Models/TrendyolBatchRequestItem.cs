using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolBatchRequestItem
{
    /// <summary>
    ///     The submitted item. Its shape depends on batchRequestType.
    /// </summary>
    [JsonPropertyName("requestItem")]
    public JsonElement RequestItem { get; set; }

    /// <summary>
    ///     SUCCESS, FAILED (or IN_PROGRESS for some operations).
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("failureReasons")]
    public List<string> FailureReasons { get; set; }

    [JsonPropertyName("batchRequestLogId")]
    public string BatchRequestLogId { get; set; }
}
