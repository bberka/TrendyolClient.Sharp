using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateUnapprovedProducts
{
    /// <summary>
    ///     Up to 1000 items.
    /// </summary>
    [JsonPropertyName("items")]
    public List<TrendyolUpdateUnapprovedProductItem> Items { get; set; }
}
