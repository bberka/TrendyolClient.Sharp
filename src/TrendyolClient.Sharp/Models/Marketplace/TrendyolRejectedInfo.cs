using System;
using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public class TrendyolRejectedInfo
{
  public long CargoTrackingNumber { get; set; }
  public string CargoProviderName { get; set; }
  public string CargoTrackingLink { get; set; }
  public long PackageId { get; set; }
  public List<Guid> Items { get; set; }
}