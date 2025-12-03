using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestSplitShipmentPackageByQuantity
{
  public List<QuantitySplit> QuantitySplit { get; set; }
}