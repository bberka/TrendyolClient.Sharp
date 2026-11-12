using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolOrderAddressLines
{
    [JsonPropertyName("addressLine1")]
    public string AddressLine1 { get; set; }

    [JsonPropertyName("addressLine2")]
    public string AddressLine2 { get; set; }
}
