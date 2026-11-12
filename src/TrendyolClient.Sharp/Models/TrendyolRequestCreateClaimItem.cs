using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCreateClaimItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>Customer return reason id; 401 (changed my mind) can be used.</summary>
    [JsonPropertyName("reasonId")]
    public int ReasonId { get; set; }

    [JsonPropertyName("customerNote")]
    public string CustomerNote { get; set; }
}
