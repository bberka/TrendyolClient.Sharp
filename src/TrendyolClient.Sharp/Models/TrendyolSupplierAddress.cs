using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Seller shipment, invoice or return address
/// </summary>
public class TrendyolSupplierAddress
{
    /// <summary>
    ///     Address ID
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    ///     Shipment, Invoice or Returning
    /// </summary>
    [JsonPropertyName("addressType")]
    public string AddressType { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("cityCode")]
    public int CityCode { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("districtId")]
    public int DistrictId { get; set; }

    [JsonPropertyName("postCode")]
    public string PostCode { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("isReturningAddress")]
    public bool IsReturningAddress { get; set; }

    [JsonPropertyName("fullAddress")]
    public string FullAddress { get; set; }

    [JsonPropertyName("isShipmentAddress")]
    public bool IsShipmentAddress { get; set; }

    [JsonPropertyName("isInvoiceAddress")]
    public bool IsInvoiceAddress { get; set; }

    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }
}
