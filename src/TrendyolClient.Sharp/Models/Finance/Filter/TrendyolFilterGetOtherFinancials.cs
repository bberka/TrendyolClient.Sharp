using System;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models.Finance.Filter;

/// <summary>
/// Filter parameters for retrieving other financial transactions
/// </summary>
public class TrendyolFilterGetOtherFinancials
{
    /// <summary>
    /// Type of financial transaction (Stoppage, CashAdvance, WireTransfer, etc.)
    /// </summary>
    public TrendyolOtherFinancialTransactionType TransactionType { get; set; }

    /// <summary>
    /// Start date for transaction records (timestamp in milliseconds)
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End date for transaction records (timestamp in milliseconds)
    /// Date range cannot exceed 15 days
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Page number for pagination (optional, default: 0)
    /// </summary>
    public long? Page { get; set; }

    /// <summary>
    /// Number of items per page (optional, default: 500, max: 1000)
    /// Can be 500 or 1000
    /// </summary>
    public long? Size { get; set; }
}