using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestExtendAgreedDeliveryDate
{
    /// <summary>Number of days to extend the agreed delivery date by: 1, 2 or 3.</summary>
    [JsonPropertyName("extendedDayCount")]
    public int ExtendedDayCount { get; set; }
}
