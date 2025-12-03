using System.Collections.Generic;
using TrendyolClient.Sharp.Models.Marketplace;

namespace TrendyolClient.Sharp.Models.Finance.Response;

public class TrendyolResponseGetCargoInvoiceDetails
{
  public long Page { get; set; }
  public long Size { get; set; }
  public long TotalPages { get; set; }
  public long TotalElements { get; set; }
  public List<TrendyolInvoiceShipmentPackage> Content { get; set; }
}