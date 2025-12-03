using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace.Request;

public class TrendyolRequestCreateClaim
{
  public List<TrendyolCreateClaimItem> ClaimItems { get; set; }
  public long CustomerId { get; set; }
  public bool ExcludeListing { get; set; }
  public bool ForcePackageCreation { get; set; }
  public string OrderNumber { get; set; }
  public long ShipmentCompanyId { get; set; }
  public string ClaimId { get; set; }
}