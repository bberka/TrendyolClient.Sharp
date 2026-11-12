using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlRequestCreateClaimAsAcceptedItem
{
    [JsonPropertyName("orderLineId")]
    public long OrderLineId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
