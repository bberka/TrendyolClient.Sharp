using System;
using TrendyolClient.Sharp.Enum;

namespace TrendyolClient.Sharp.Models.Finance.Filter;

public sealed class FilterGetSettlements
{
  public SettlementsTransactionType TransactionType { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public long? Page { get; set; }
  public long? Size { get; set; }
}