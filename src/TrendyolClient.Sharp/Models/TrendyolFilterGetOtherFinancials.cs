using System.Collections.Generic;
using Refit;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Filter parameters for the otherfinancials service.
///     One of the date filters is required: StartDate and EndDate (max 15 days apart), PaymentDate, or PaymentOrderId.
/// </summary>
public class TrendyolFilterGetOtherFinancials
{
    /// <summary>
    ///     Single transaction type. Either TransactionType or TransactionTypes is required; TransactionTypes wins if both are set
    /// </summary>
    public TrendyolOtherFinancialTransactionType? TransactionType { get; set; }

    /// <summary>
    ///     Multiple transaction types, sent comma separated
    /// </summary>
    [Query(CollectionFormat.Csv)]
    public List<TrendyolOtherFinancialTransactionType> TransactionTypes { get; set; }

    /// <summary>
    ///     Narrows DeductionInvoices records (e.g. PlatformServiceFee). TransactionType or TransactionTypes must include DeductionInvoices
    /// </summary>
    public TrendyolOtherFinancialTransactionSubType? TransactionSubType { get; set; }

    /// <summary>
    ///     Start of the transaction date range (unix timestamp, milliseconds). Required with EndDate unless PaymentDate or PaymentOrderId is set
    /// </summary>
    public long? StartDate { get; set; }

    /// <summary>
    ///     End of the transaction date range (unix timestamp, milliseconds). Must not be more than 15 days after StartDate
    /// </summary>
    public long? EndDate { get; set; }

    /// <summary>
    ///     Earliest date a record can enter payment, formatted yyyy-MM-dd (Europe/Istanbul). Filters by payment/due date instead of
    ///     transaction date; StartDate and EndDate are not used
    /// </summary>
    public string PaymentDate { get; set; }

    /// <summary>
    ///     Payment order ID from the payment-order service. Fetches the records of a payout period without StartDate/EndDate
    /// </summary>
    public long? PaymentOrderId { get; set; }

    /// <summary>
    ///     Page number, starting from 0
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    ///     Page size: 500 (default) or 1000
    /// </summary>
    public int? Size { get; set; }
}
