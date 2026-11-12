using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUpdateContentBulk
{
    /// <summary>
    ///     Up to 1000 items.
    /// </summary>
    [JsonPropertyName("items")]
    public List<TrendyolUpdateContentItem> Items { get; set; }
}
