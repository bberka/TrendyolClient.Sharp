using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Shipment or invoice address of an order package.
/// </summary>
public class TrendyolOrderAddress
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    /// <summary>
    ///     May be empty for GULF region orders.
    /// </summary>
    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonPropertyName("address1")]
    public string Address1 { get; set; }

    [JsonPropertyName("address2")]
    public string Address2 { get; set; }

    [JsonPropertyName("addressLines")]
    public TrendyolOrderAddressLines AddressLines { get; set; }

    [JsonPropertyName("fullAddress")]
    public string FullAddress { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("cityCode")]
    public int? CityCode { get; set; }

    /// <summary>
    ///     May be empty for GULF region orders.
    /// </summary>
    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("districtId")]
    public int? DistrictId { get; set; }

    [JsonPropertyName("neighborhood")]
    public string Neighborhood { get; set; }

    [JsonPropertyName("neighborhoodId")]
    public int? NeighborhoodId { get; set; }

    /// <summary>
    ///     Populated for the CEE region.
    /// </summary>
    [JsonPropertyName("countyId")]
    public int? CountyId { get; set; }

    /// <summary>
    ///     Populated for the CEE region.
    /// </summary>
    [JsonPropertyName("countyName")]
    public string CountyName { get; set; }

    /// <summary>
    ///     Populated for the GULF region.
    /// </summary>
    [JsonPropertyName("shortAddress")]
    public string ShortAddress { get; set; }

    /// <summary>
    ///     Populated for the GULF region.
    /// </summary>
    [JsonPropertyName("stateName")]
    public string StateName { get; set; }

    /// <summary>
    ///     May be empty for GULF region orders.
    /// </summary>
    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("sector")]
    public string Sector { get; set; }

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("latitude")]
    public string Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public string Longitude { get; set; }

    /// <summary>
    ///     Invoice address only; returned when <c>commercial</c> is true.
    /// </summary>
    [JsonPropertyName("taxOffice")]
    public string TaxOffice { get; set; }

    /// <summary>
    ///     Invoice address only; returned when <c>commercial</c> is true.
    /// </summary>
    [JsonPropertyName("taxNumber")]
    public string TaxNumber { get; set; }

    /// <summary>
    ///     Invoice address only; whether the commercial customer is an e-invoice taxpayer.
    /// </summary>
    [JsonPropertyName("eInvoiceAvailable")]
    public bool? EInvoiceAvailable { get; set; }
}
