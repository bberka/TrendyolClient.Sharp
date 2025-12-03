using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestSplitShipmentPackageByQuantity
{
    [JsonPropertyName("quantitySplit")]
    public List<TrendyolQuantitySplitItem> QuantitySplit { get; set; }
}