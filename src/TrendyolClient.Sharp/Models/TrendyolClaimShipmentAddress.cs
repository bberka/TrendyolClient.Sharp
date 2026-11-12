using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimShipmentAddress
{
    [JsonPropertyName("address1")]
    public string Address1 { get; set; }

    [JsonPropertyName("address2")]
    public string Address2 { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }
}
