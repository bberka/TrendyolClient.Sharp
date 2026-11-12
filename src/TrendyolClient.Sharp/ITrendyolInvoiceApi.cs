using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Seller invoice integration: send an invoice link, delete it, or upload the invoice file.
///     https://developers.trendyol.com/docs/fatura-linki-gonderme-sendinvoicelink
/// </summary>
public interface ITrendyolInvoiceApi
{
    /// <summary>
    ///     Sends the invoice link of a shipment package.
    /// </summary>
    [Post("/integration/sellers/SELLER_ID_PARAMETER/seller-invoice-links")]
    Task<IApiResponse> SendInvoiceLinkAsync([Body] TrendyolRequestSendInvoiceLink request);

    /// <summary>
    ///     Deletes a previously sent e-archive invoice link.
    /// </summary>
    [Post("/integration/sellers/SELLER_ID_PARAMETER/seller-invoice-links/delete")]
    Task<IApiResponse> DeleteInvoiceLinkAsync([Body] TrendyolRequestDeleteInvoiceLink request);

    /// <summary>
    ///     Uploads the invoice file (PDF, JPEG or PNG, max 10 MB) of a shipment package as multipart/form-data.
    ///     Future-dated invoices are rejected, and a package that already has an invoice returns 409.
    ///     E-archive invoices cannot be sent for Trendyol overseas intermediary orders (send an e-invoice).
    ///     Parts are sent as plain text so the values are not JSON-encoded; pass numbers as their decimal string.
    /// </summary>
    /// <param name="shipmentPackageId">Shipment package ID (required)</param>
    /// <param name="file">Invoice file. Example: <c>new StreamPart(stream, "invoice.pdf", "application/pdf")</c></param>
    /// <param name="invoiceDateTime">Invoice date as a unix timestamp in seconds (10 digits) or milliseconds (13 digits). Required for micro export orders</param>
    /// <param name="invoiceNumber">Invoice number: 3 alphanumeric chars, 4-digit year (2020-2099), 9 digits. Required for micro export orders</param>
    [Multipart]
    [Post("/integration/sellers/SELLER_ID_PARAMETER/seller-invoice-file")]
    Task<IApiResponse> UploadInvoiceFileAsync(
        [AliasAs("shipmentPackageId")] string shipmentPackageId,
        [AliasAs("file")] StreamPart file,
        [AliasAs("invoiceDateTime")] string invoiceDateTime = null,
        [AliasAs("invoiceNumber")] string invoiceNumber = null
    );
}
