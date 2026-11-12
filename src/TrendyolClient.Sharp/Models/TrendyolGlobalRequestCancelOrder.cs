using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalRequestCancelOrder
{
    /// <summary>
    ///     Products to cancel (max 100)
    /// </summary>
    [JsonPropertyName("items")]
    public List<TrendyolGlobalRequestCancelOrderItem> Items { get; set; }
}
