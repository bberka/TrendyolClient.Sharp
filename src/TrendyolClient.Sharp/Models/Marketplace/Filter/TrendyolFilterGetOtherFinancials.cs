using System;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models.Marketplace.Filter;

public class TrendyolFilterGetOtherFinancials
{
  public TrendyolOtherFinancialTransactionType TransactionType { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public long? Page { get; set; }
  public long? Size { get; set; }
}