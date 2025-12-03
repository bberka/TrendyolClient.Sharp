using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Finance.Response;

public class TrendyolResponseGetOtherFinancials
{
  public long Page { get; set; }
  public long Size { get; set; }
  public long TotalPages { get; set; }
  public long TotalElements { get; set; }
  public List<TrendyolOtherFinancialsTransactionData> Content { get; set; }
}