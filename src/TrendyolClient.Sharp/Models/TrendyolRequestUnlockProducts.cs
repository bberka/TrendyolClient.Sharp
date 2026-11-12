using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestUnlockProducts
{
    [JsonPropertyName("items")]
    public List<TrendyolUnlockProductItem> Items { get; set; }
}
