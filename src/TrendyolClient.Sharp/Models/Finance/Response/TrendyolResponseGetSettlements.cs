using System.Collections.Generic;

namespace TrendyolClient.Sharp.Models.Finance.Response;

public sealed class ResponseGetSettlements
{
  public long Page { get; set; }
  public long Size { get; set; }
  public long TotalPages { get; set; }
  public long TotalElements { get; set; }
  public List<SettlementTransactionData> Content { get; set; }
}