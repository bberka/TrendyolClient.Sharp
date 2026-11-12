using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Seller information: addresses, warranty documents and the stage environment IP whitelist.
/// </summary>
public interface ITrendyolSellerApi
{
    /// <summary>
    ///     Gets the seller's shipment, invoice and return addresses with the default addresses. The address IDs are used
    ///     by the product create V2 request. Do not use this service until "Satici Basvuru Surecim" is fully completed.
    ///     https://developers.trendyol.com/docs/iade-ve-sevkiyat-adres-bilgileri-getsuppliersaddresses
    /// </summary>
    /// <param name="storeFrontCode">Country code the seller sells in. Trendyol defaults to "TR" when omitted</param>
    [Get("/integration/sellers/SELLER_ID_PARAMETER/addresses")]
    Task<IApiResponse<TrendyolResponseGetAddresses>> GetSuppliersAddressesAsync([Header("storeFrontCode")] string storeFrontCode = null);

    /// <summary>
    ///     Attaches a warranty certificate PDF to an order line, enabling "Garanti Belgesini Gor" in the customer app.
    ///     The sent document cannot be viewed back through the seller panel or the API.
    ///     https://developers.trendyol.com/docs/garanti-belgesi-gonderimi
    /// </summary>
    /// <param name="shipmentPackageId">Shipment package ID (required)</param>
    /// <param name="lineId">Order line ID of the product, from the order packages service (required)</param>
    /// <param name="file">Warranty certificate PDF. Example: <c>new StreamPart(stream, "warranty.pdf", "application/pdf")</c></param>
    [Multipart]
    [Post("/integration/sellers/SELLER_ID_PARAMETER/warranty-documents")]
    Task<IApiResponse> SendWarrantyDocumentAsync(
        [AliasAs("shipmentPackageId")] string shipmentPackageId,
        [AliasAs("lineId")] string lineId,
        [AliasAs("file")] StreamPart file
    );

    /// <summary>
    ///     Lists the IPv4 addresses whitelisted for stage environment access (at most 7).
    ///     The documentation only lists the production host (apigw.trendyol.com) for this service.
    ///     https://developers.trendyol.com/docs/test-ortam-ip-whitelist-yonetimi
    /// </summary>
    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/ip-whitelists")]
    Task<IApiResponse<TrendyolResponseGetIpWhitelists>> GetIpWhitelistsAsync();

    /// <summary>
    ///     Adds an IPv4 address to the stage whitelist. Limits: 7 active IPs and 7 additions per day (429 when exceeded);
    ///     409 if the IP is already active; 422 if it fails the security check. The entry expires at the returned expiresAt.
    /// </summary>
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/ip-whitelists")]
    Task<IApiResponse<TrendyolResponseAddIpWhitelist>> AddIpWhitelistAsync([Body] TrendyolRequestAddIpWhitelist request);

    /// <summary>
    ///     Removes an IPv4 address from the stage whitelist (204 on success, 404 if not found).
    ///     Deleting frees one of the 7 slots but does not reset the daily addition limit.
    /// </summary>
    /// <param name="ip">IPv4 address to remove</param>
    [Delete("/integration/product/sellers/SELLER_ID_PARAMETER/ip-whitelists/{ip}")]
    Task<IApiResponse> DeleteIpWhitelistAsync([AliasAs("ip")] string ip);
}
