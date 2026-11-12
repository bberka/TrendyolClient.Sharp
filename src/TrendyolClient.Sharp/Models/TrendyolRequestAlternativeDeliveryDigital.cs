using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Request to deliver a digital product code (only for lines whose businessUnit is "Digital Goods").
/// </summary>
public class TrendyolRequestAlternativeDeliveryDigital
{
    /// <summary>
    ///     Must be true.
    /// </summary>
    [JsonPropertyName("isPhoneNumber")]
    public bool IsPhoneNumber { get; set; }

    /// <summary>
    ///     Customer contact phone number.
    /// </summary>
    [JsonPropertyName("trackingInfo")]
    public string TrackingInfo { get; set; }

    [JsonPropertyName("params")]
    public TrendyolDigitalDeliveryParams Params { get; set; }
}
