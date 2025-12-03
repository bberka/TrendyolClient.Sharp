using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCreateTestOrder
{
    [JsonPropertyName("customer")]
    public TrendyolCustomer Customer { get; set; }

    [JsonPropertyName("invoiceAddress")]
    public TrendyolInvoiceAddress InvoiceAddress { get; set; }

    [JsonPropertyName("lines")]
    public List<TrendyolOrderLine> Lines { get; set; }

    [JsonPropertyName("seller")]
    public TrendyolOrderSeller Seller { get; set; }

    [JsonPropertyName("shippingAddress")]
    public TrendyolShippingAddress ShippingAddress { get; set; }

    [JsonPropertyName("commercial")]
    public bool Commercial { get; set; }

    [JsonPropertyName("microRegion")]
    public string MicroRegion { get; set; }
}