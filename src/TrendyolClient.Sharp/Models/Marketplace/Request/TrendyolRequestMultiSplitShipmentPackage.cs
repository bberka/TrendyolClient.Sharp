using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestMultiSplitShipmentPackage
{
  public List<TrendyolSplitGroup> SplitGroups { get; set; }
}