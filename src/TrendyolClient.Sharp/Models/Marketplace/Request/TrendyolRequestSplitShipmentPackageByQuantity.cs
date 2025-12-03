using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestSplitShipmentPackageByQuantity
{
  public List<TrendyolQuantitySplit> QuantitySplit { get; set; }
}