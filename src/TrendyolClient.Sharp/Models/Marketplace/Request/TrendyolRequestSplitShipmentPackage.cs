using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public sealed class RequestSplitShipmentPackage
{
  public List<long> OrderLineIds { get; set; }
}