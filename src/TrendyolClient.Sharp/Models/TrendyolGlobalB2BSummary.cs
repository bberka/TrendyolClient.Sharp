using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalB2BSummary
{
    [JsonPropertyName("b2BId")]
    public string B2BId { get; set; }

    [JsonPropertyName("invoiceNumber")]
    public string InvoiceNumber { get; set; }

    [JsonPropertyName("warehouseId")]
    public string WarehouseId { get; set; }

    [JsonPropertyName("warehouseName")]
    public string WarehouseName { get; set; }

    /// <summary>
    ///     RFC3339 date-time
    /// </summary>
    [JsonPropertyName("createDate")]
    public string CreateDate { get; set; }
}
