using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimItem
{
    [JsonPropertyName("claimLineItemId")]
    public string ClaimLineItemId { get; set; }

    [JsonPropertyName("orderLineId")]
    public long OrderLineId { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}