using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlClaimShipmentAddress
{
    [JsonPropertyName("address1")]
    public string Address1 { get; set; }

    [JsonPropertyName("address2")]
    public string Address2 { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    /// <summary>Customer phone number, returned for 4 days after the return was rejected.</summary>
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}
