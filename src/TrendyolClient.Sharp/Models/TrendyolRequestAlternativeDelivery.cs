using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestAlternativeDelivery
{
    [JsonPropertyName("isPhoneNumber")]
    public bool IsPhoneNumber { get; set; }

    [JsonPropertyName("trackingInfo")]
    public string TrackingInfo { get; set; }

    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; set; }

    [JsonPropertyName("boxQuantity")]
    public int? BoxQuantity { get; set; }

    [JsonPropertyName("deci")]
    public decimal? Deci { get; set; }
}