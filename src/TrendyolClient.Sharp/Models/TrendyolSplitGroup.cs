using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolSplitGroup
{
    [JsonPropertyName("orderLineIds")]
    public List<long> OrderLineIds { get; set; }
}