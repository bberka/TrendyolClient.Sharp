using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalVendorRefSummary
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; }

    [JsonPropertyName("vendorRef")]
    public string VendorRef { get; set; }

    /// <summary>
    ///     RFC3339 date-time
    /// </summary>
    [JsonPropertyName("creationDate")]
    public string CreationDate { get; set; }
}
