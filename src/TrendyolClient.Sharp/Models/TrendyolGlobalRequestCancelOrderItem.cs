using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalRequestCancelOrderItem
{
    [JsonPropertyName("variantId")]
    public string VariantId { get; set; }

    /// <summary>
    ///     Quantity to cancel (min 1)
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
