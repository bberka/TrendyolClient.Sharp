using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestMultiSplitShipmentPackage
{
  public List<TrendyolSplitGroup> SplitGroups { get; set; }
}