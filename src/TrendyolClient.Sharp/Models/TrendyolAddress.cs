using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolAddress
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("addressType")]
    public string AddressType { get; set; }

    [JsonPropertyName("fullAddress")]
    public string FullAddress { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }
}