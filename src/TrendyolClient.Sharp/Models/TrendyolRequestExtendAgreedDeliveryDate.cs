using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestExtendAgreedDeliveryDate
{
    [JsonPropertyName("extendedDayCount")]
    public int ExtendedDayCount { get; set; }
}