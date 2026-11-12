using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalResponseCreateOrder
{
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>
    ///     RFC3339 date-time
    /// </summary>
    [JsonPropertyName("createDateTime")]
    public string CreateDateTime { get; set; }

    [JsonPropertyName("items")]
    public List<TrendyolGlobalOrderItem> Items { get; set; }
}
