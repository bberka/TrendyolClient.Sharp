using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Marketplace;

public sealed class Claim
{
  public string Id { get; set; }
  public string OrderNumber { get; set; }
  public long OrderDate { get; set; }
  public string CustomerFirstName { get; set; }
  public string CustomerLastName { get; set; }
  public long ClaimDate { get; set; }
  public long CargoTrackingNumber { get; set; }
  public string CargoTrackingLink { get; set; }
  public string CargoSenderNumber { get; set; }
  public string CargoProviderName { get; set; }
  public long OrderShipmentPackageId { get; set; }
  public TrendyolReplacementInfo ReplacementOutboundpackageinfo { get; set; }
  public RejectedInfo RejectedPackageInfo { get; set; }
  public List<ClaimItem> Items { get; set; }
}