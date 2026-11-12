using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of the International test order services (Europe /orders/eu and GULF /orders/local-gulf).</summary>
public class TrendyolIntlRequestCreateTestOrder
{
    [JsonPropertyName("customer")]
    public TrendyolCustomer Customer { get; set; }

    [JsonPropertyName("invoiceAddress")]
    public TrendyolIntlTestOrderInvoiceAddress InvoiceAddress { get; set; }

    [JsonPropertyName("lines")]
    public List<TrendyolIntlTestOrderLine> Lines { get; set; }

    [JsonPropertyName("shippingAddress")]
    public TrendyolIntlTestOrderShippingAddress ShippingAddress { get; set; }
}
