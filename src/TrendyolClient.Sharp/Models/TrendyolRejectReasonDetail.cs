using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRejectReasonDetail
{
    [JsonPropertyName("rejectReason")]
    public string RejectReason { get; set; }

    [JsonPropertyName("rejectReasonDetail")]
    public string RejectReasonDetail { get; set; }
}
