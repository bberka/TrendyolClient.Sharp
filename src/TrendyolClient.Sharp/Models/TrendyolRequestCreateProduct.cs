using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCreateProduct
{
    /// <summary>
    ///     Up to 1000 items.
    /// </summary>
    [JsonPropertyName("items")]
    public List<TrendyolCreateProductItem> Items { get; set; }
}
