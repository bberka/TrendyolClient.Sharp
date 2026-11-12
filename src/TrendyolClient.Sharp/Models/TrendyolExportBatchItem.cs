using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportBatchItem
{
    /// <summary>
    ///     Echo of the submitted item; the shape depends on the batch type
    /// </summary>
    [JsonPropertyName("requestItem")]
    public JsonElement RequestItem { get; set; }

    /// <summary>
    ///     SUCCESS, FAILED or PENDING
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("failureReasons")]
    public List<string> FailureReasons { get; set; }
}
