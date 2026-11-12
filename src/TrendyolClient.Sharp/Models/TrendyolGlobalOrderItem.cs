using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalOrderItem
{
    [JsonPropertyName("variantId")]
    public string VariantId { get; set; }

    /// <summary>
    ///     Total quantity
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("cancelable")]
    public bool Cancelable { get; set; }

    [JsonPropertyName("lineItems")]
    public List<TrendyolGlobalOrderLineItem> LineItems { get; set; }
}
