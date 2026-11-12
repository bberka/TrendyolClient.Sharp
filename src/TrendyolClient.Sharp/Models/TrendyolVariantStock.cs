using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolVariantStock
{
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    ///     Null when the stock was never updated.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public long? LastModifiedDate { get; set; }
}
