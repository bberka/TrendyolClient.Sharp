namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     Transaction types accepted by the otherfinancials service. Names are sent to the API as-is.
/// </summary>
public enum TrendyolOtherFinancialTransactionType
{
    Stoppage,
    CashAdvance,
    WireTransfer,
    IncomingTransfer,
    ReturnInvoice,
    CommissionAgreementInvoice,
    PaymentOrder,
    DeductionInvoices,
    FinancialItem
}
