using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolInvoiceAddress
{
    [JsonPropertyName("addressText")]
    public string AddressText { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("invoiceFirstName")]
    public string InvoiceFirstName { get; set; }

    [JsonPropertyName("invoiceLastName")]
    public string InvoiceLastName { get; set; }

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

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("invoiceTaxNumber")]
    public string InvoiceTaxNumber { get; set; }

    [JsonPropertyName("invoiceTaxOffice")]
    public string InvoiceTaxOffice { get; set; }
}