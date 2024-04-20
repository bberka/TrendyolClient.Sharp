﻿using System;
using TrendyolSharp.Enum;

namespace TrendyolSharp.Models.Marketplace.Filter
{
  public sealed class FilterGetOtherFinancials
  {
    public OtherFinancialTransactionType TransactionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long? Page { get; set; }
    public long? Size { get; set; }
  }
}