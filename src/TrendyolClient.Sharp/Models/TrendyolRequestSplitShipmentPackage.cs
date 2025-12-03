using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestSplitShipmentPackage
{
    [JsonPropertyName("orderLineIds")]
    public List<long> OrderLineIds { get; set; }
}