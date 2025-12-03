using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestSplitShipmentPackage
{
  public List<long> OrderLineIds { get; set; }
}