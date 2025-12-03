using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolShippingAddress
{
    [JsonPropertyName("addressText")]
    public string AddressText { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("latitude")]
    public string Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public string Longitude { get; set; }

    [JsonPropertyName("neighborhood")]
    public string Neighborhood { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("shippingFirstName")]
    public string ShippingFirstName { get; set; }

    [JsonPropertyName("shippingLastName")]
    public string ShippingLastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}