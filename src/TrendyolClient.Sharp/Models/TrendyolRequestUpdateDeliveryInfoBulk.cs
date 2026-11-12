using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateDeliveryInfoBulk
{
    /// <summary>
    ///     Up to 1000 items.
    /// </summary>
    [JsonPropertyName("items")]
    public List<TrendyolUpdateDeliveryInfoItem> Items { get; set; }
}
