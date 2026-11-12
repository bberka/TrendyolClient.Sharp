using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlRequestCreateClaimAsAcceptedItem
{
    /// <summary>Id of the order line (orderLine.id).</summary>
    [JsonPropertyName("orderLineId")]
    public long OrderLineId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
