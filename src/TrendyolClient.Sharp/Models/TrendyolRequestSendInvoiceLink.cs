using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestSendInvoiceLink
{
    [JsonPropertyName("invoiceLink")]
    public string InvoiceLink { get; set; }

    [JsonPropertyName("shipmentPackageId")]
    public long ShipmentPackageId { get; set; }

    [JsonPropertyName("invoiceDateTime")]
    public long InvoiceDateTime { get; set; }

    [JsonPropertyName("invoiceNumber")]
    public string InvoiceNumber { get; set; }
}