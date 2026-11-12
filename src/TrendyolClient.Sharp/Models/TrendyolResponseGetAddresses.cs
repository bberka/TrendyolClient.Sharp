using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetAddresses
{
    [JsonPropertyName("supplierAddresses")]
    public List<TrendyolSupplierAddress> SupplierAddresses { get; set; }

    [JsonPropertyName("defaultShipmentAddress")]
    public TrendyolSupplierAddress DefaultShipmentAddress { get; set; }

    [JsonPropertyName("defaultInvoiceAddress")]
    public TrendyolSupplierAddress DefaultInvoiceAddress { get; set; }

    [JsonPropertyName("defaultReturningAddress")]
    public TrendyolDefaultReturningAddress DefaultReturningAddress { get; set; }
}
