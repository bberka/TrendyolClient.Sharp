using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Trendyol Finance API endpoints for financial records and transactions
///     https://developers.trendyol.com/docs/marketplace/finans-entegrasyonu
/// </summary>
public interface ITrendyolFinanceApi
{
    /// <summary>
    ///     Gets settlement transactions (Sales, Returns, Discounts, Coupons, Provisions).
    ///     Financial records are created after the order is delivered.
    ///     Date range cannot exceed 15 days.
    ///     https://developers.trendyol.com/docs/marketplace/finans-entegrasyonu/cari-hesap-ekstresi
    /// </summary>
    /// <param name="filter">Filter parameters including transaction type, date range, and pagination</param>
    /// <returns>Paginated list of settlement transactions</returns>
    [Get("/integration/finance/che/sellers/SELLER_ID_PARAMETER/settlements")]
    Task<IApiResponse<TrendyolResponseGetSettlements>> GetSettlementsAsync([Query] TrendyolFilterGetSettlements filter);

    /// <summary>
    ///     Gets other financial transactions (Supplier financing, transfers, payments, invoices).
    ///     Includes: Stoppage, CashAdvance, WireTransfer, IncomingTransfer, ReturnInvoice,
    ///     CommissionAgreementInvoice, PaymentOrder, DeductionInvoices, FinancialItem.
    ///     Date range cannot exceed 15 days.
    ///     https://developers.trendyol.com/docs/marketplace/finans-entegrasyonu/diger-finansal-islemler
    /// </summary>
    /// <param name="filter">Filter parameters including transaction type, date range, and pagination</param>
    /// <returns>Paginated list of other financial transactions</returns>
    [Get("/integration/finance/che/sellers/SELLER_ID_PARAMETER/otherfinancials")]
    Task<IApiResponse<TrendyolResponseGetOtherFinancials>> GetOtherFinancialsAsync([Query] TrendyolFilterGetOtherFinancials filter);

    /// <summary>
    ///     Gets cargo invoice details for shipment packages.
    ///     https://developers.trendyol.com/docs/marketplace/finans-entegrasyonu/kargo-faturasi-detaylari
    /// </summary>
    /// <param name="invoiceSerialNumber">Invoice serial number</param>
    /// <returns>Cargo invoice details with shipment packages</returns>
    [Get("/integration/finance/che/sellers/SELLER_ID_PARAMETER/cargo-invoice/{invoiceSerialNumber}/items")]
    Task<IApiResponse<TrendyolResponseGetCargoInvoiceDetails>> GetCargoInvoiceDetailsAsync(
        [AliasAs("invoiceSerialNumber")] string invoiceSerialNumber
    );
}