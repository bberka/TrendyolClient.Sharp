using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateTrackingNumber
{
    [JsonPropertyName("trackingNumber")]
    public string TrackingNumber { get; set; }
}