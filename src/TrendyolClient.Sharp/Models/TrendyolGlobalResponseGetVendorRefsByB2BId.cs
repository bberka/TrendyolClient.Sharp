using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalResponseGetVendorRefsByB2BId
{
    [JsonPropertyName("b2BId")]
    public long B2BId { get; set; }

    [JsonPropertyName("invoiceNumber")]
    public string InvoiceNumber { get; set; }

    [JsonPropertyName("vendorRefSummaries")]
    public List<TrendyolGlobalVendorRefSummary> VendorRefSummaries { get; set; }
}
