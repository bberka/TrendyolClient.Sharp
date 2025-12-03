using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Filter;

public sealed class FilterGetShipmentPackages
{
  public long? StartDate { get; set; }
  public long? EndDate { get; set; }
  public long? Page { get; set; }
  public long? Size { get; set; }
  public long? SupplierId { get; set; }
  public long? OrderNumber { get; set; }
  public string Status { get; set; }
  public string OrderByField { get; set; }
  public string OrderByDirection { get; set; }
  public List<long> ShipmentPackageIds { get; set; }
}