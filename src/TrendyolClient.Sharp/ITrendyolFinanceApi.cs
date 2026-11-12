using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Trendyol accounting and finance (current account statement) integration.
///     https://developers.trendyol.com/docs/cari-hesap-ekstresi-entegrasyonu
///     Settlements, otherfinancials and payment-order return separate sets of records.
/// </summary>
public interface ITrendyolFinanceApi
{
    /// <summary>
    ///     Gets sales, return, discount, coupon, provision and delivery fee records (settlements).
    ///     Records are created after the order is delivered. Send a transaction type (or types) and exactly one date
    ///     scope: StartDate+EndDate (max 15 days apart), PaymentDate, or PaymentOrderId.
    /// </summary>
    /// <param name="filter">Transaction type(s), date scope and paging</param>
    [Get("/integration/finance/che/sellers/SELLER_ID_PARAMETER/settlements")]
    Task<IApiResponse<TrendyolResponseGetSettlements>> GetSettlementsAsync([Query] TrendyolFilterGetSettlements filter);

    /// <summary>
    ///     Gets stoppage, cash advance, transfer, payment, invoice and financial adjustment records (otherfinancials).
    ///     Same date rules as settlements. Use TransactionSubType = PlatformServiceFee together with DeductionInvoices
    ///     to list platform service fee invoices.
    /// </summary>
    /// <param name="filter">Transaction type(s), sub type, date scope and paging</param>
    [Get("/integration/finance/che/sellers/SELLER_ID_PARAMETER/otherfinancials")]
    Task<IApiResponse<TrendyolResponseGetOtherFinancials>> GetOtherFinancialsAsync([Query] TrendyolFilterGetOtherFinancials filter);

    /// <summary>
    ///     Lists the seller's paid payment orders, 10 per page at most. The returned id is the paymentOrderId filter of
    ///     <see cref="GetSettlementsAsync" /> and <see cref="GetOtherFinancialsAsync" />, which then need no date range.
    /// </summary>
    /// <param name="filter">Paging parameters</param>
    [Get("/integration/finance/che/sellers/SELLER_ID_PARAMETER/payment-order")]
    Task<IApiResponse<TrendyolResponseGetPaymentOrders>> GetPaymentOrdersAsync([Query] TrendyolFilterGetPaymentOrders filter = null);

    /// <summary>
    ///     Gets the line items of a cargo invoice. The invoice serial number is the Id of the otherfinancials
    ///     (DeductionInvoices) record whose transactionType is "Kargo Faturası" / "Kargo Fatura".
    /// </summary>
    /// <param name="invoiceSerialNumber">Cargo invoice serial number</param>
    /// <param name="filter">Paging parameters</param>
    [Get("/integration/finance/che/sellers/SELLER_ID_PARAMETER/cargo-invoice/{invoiceSerialNumber}/items")]
    Task<IApiResponse<TrendyolResponseGetCargoInvoiceDetails>> GetCargoInvoiceDetailsAsync(
        [AliasAs("invoiceSerialNumber")] string invoiceSerialNumber,
        [Query] TrendyolFilterGetCargoInvoiceItems filter = null
    );
}
