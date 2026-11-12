using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Shipping address of an International test order.</summary>
public class TrendyolIntlTestOrderShippingAddress
{
    [JsonPropertyName("addressText")]
    public string AddressText { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("latitude")]
    public string Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public string Longitude { get; set; }

    [JsonPropertyName("neighborhood")]
    public string Neighborhood { get; set; }

    /// <summary>10 digits, numbers only.</summary>
    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("shippingFirstName")]
    public string ShippingFirstName { get; set; }

    [JsonPropertyName("shippingLastName")]
    public string ShippingLastName { get; set; }

    /// <summary>GULF endpoint only (documented in the guide sample, not in the reference).</summary>
    [JsonPropertyName("shortAddress")]
    public string ShortAddress { get; set; }
}
