using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Enums;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

public interface ITrendyolCommonLabelApi
{
    /// <summary>
    ///     Requests the common label (TEX and Aras Kargo, Trendyol-paid shipments). Call it after the package reached
    ///     Picking or Invoiced; the ZPL label is then available from <see cref="GetCommonLabelAsync" />. Returns no body.
    /// </summary>
    [Post("/integration/sellers/SELLER_ID_PARAMETER/common-label/{cargoTrackingNumber}")]
    Task<IApiResponse> CreateCommonLabelAsync(
        [AliasAs("cargoTrackingNumber")] string cargoTrackingNumber,
        [Body] TrendyolRequestCreateCommonLabel request
    );

    /// <summary>Gets the label requested with createCommonLabel (one ZPL per box).</summary>
    [Get("/integration/sellers/SELLER_ID_PARAMETER/common-label/{cargoTrackingNumber}")]
    Task<IApiResponse<TrendyolResponseGetCommonLabel>> GetCommonLabelAsync([AliasAs("cargoTrackingNumber")] string cargoTrackingNumber);

    /// <summary>
    ///     Gets the EU product label(s) together with the cargo label, for Trendyol Overseas Mediation (Yurt Dışı
    ///     Aracılığı) orders only. To also receive the cargo label, call createCommonLabel first. The product label comes in the
    ///     requested format; the cargo label is always ZPL.
    /// </summary>
    [Get("/integration/sellers/SELLER_ID_PARAMETER/common-labels/{cargoTrackingNumber}/with-product-labels")]
    Task<IApiResponse<TrendyolResponseGetCommonLabel>> GetCommonLabelWithProductLabelsAsync(
        [AliasAs("cargoTrackingNumber")] string cargoTrackingNumber,
        [Query] TrendyolProductLabelType productLabelType
    );
}
