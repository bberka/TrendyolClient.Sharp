using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     International Marketplace order integration (Germany, CEE and GULF; v3.0 docs): shipment packages, package
///     operations, the order webhook and the stage-only test order helpers. The storefront (DE, SA, AE, KW, GR, SK, RO, CZ, BG)
///     is set once when the client is created (<c>GetOrCreateInternationalClient</c>) and sent as the <c>storeFrontCode</c> header.
///     Methods that are identical to the Türkiye client are not repeated here: <c>CreateCommonLabelAsync</c> /
///     <c>GetCommonLabelAsync</c> (<see cref="ITrendyolCommonLabelApi" />) and
///     <c>ReturnTestOrderToWaitingInActionAsync</c> (<see cref="ITrendyolOrderApi" />).
/// </summary>
public interface ITrendyolInternationalOrderApi
{    /// <summary>
    ///     Notifies the tracking number of a Seller Pays package (updateTrackingNumber). The package must be
    ///     in Picking. Also usable for rejected return packages (Europe and GULF) with the rejected package id.
    ///     Fails with "Shipment tracking number cannot be updated" in Delivered, UnDelivered and Returned, and in
    ///     GULF/rejected packages when a number was already given. <c>ReturnTrackingNumber</c> is Germany only.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/tracking-details")]
    Task<IApiResponse> UpdateIntlTrackingNumberAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolIntlRequestUpdateTrackingNumber request
    );

    /// <summary>
    ///     Cancels items of a package (cancelPackage, path ".../items/unsupplied"). The package is split and a new
    ///     package id is generated. Differs from the Türkiye call by the storefront header, reason 503 and
    ///     <c>shouldKeepPreviousStatus</c>.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/items/unsupplied")]
    Task<IApiResponse> CancelIntlOrderPackageItemAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolIntlRequestCancelOrderPackageItem request
    );

    /// <summary>
    ///     Splits a package into several packages by quantity per order line (splitMultiPackagesByQuantity). New
    ///     packages are created asynchronously in UnPacked status; lines left out go into a separate package.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/split-packages")]
    Task<IApiResponse> SplitIntlMultiPackageByQuantityAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolIntlRequestSplitMultiPackageByQuantity request
    );

    /// <summary>
    ///     Moves the given order lines into new packages (splitShipmentPackage). New package ids are generated;
    ///     fetch the packages again afterwards.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/split")]
    Task<IApiResponse> SplitIntlShipmentPackageAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolIntlRequestSplitShipmentPackage request
    );

    /// <summary>
    ///     Splits a package into several packages in one request (multiSplitShipmentPackage). Do not send every line;
    ///     the remaining lines get their own package automatically. Created asynchronously.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/multi-split")]
    Task<IApiResponse> MultiSplitIntlShipmentPackageAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolIntlRequestMultiSplitShipmentPackage request
    );

    /// <summary>
    ///     Splits the quantity of order lines into several packages (splitShipmentPackageByQuantity).
    ///     <c>quantities [2,2]</c> on a line of 4 creates two packages of 2. Created asynchronously.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/quantity-split")]
    Task<IApiResponse> SplitIntlShipmentPackageByQuantityAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolIntlRequestSplitShipmentPackageByQuantity request
    );

    /// <summary>
    ///     Feeds deci and box quantity (updateBoxInfo, answers 202). Required for Flow PL and Starlinks H&amp;B (GULF)
    ///     and KLG large parcels (Romania, where width/length/height in cm are mandatory too). Trendyol Pays only.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/box-info")]
    Task<IApiResponse> NotifyIntlBoxInfoAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolIntlRequestNotifyBoxInfo request
    );

    /// <summary>
    ///     STAGE only. Creates a test order for the CEE regions and returns its order number. The SellerID header must
    ///     be the test seller; products must be approved. The request may carry invoice tax number and office.
    /// </summary>
    [Post("/integration/test/order/orders/eu")]
    [Headers("SellerID: SELLER_ID_PARAMETER")]
    Task<IApiResponse<TrendyolResponseCreateTestOrder>> CreateIntlTestOrderEuropeAsync(
        [Body] TrendyolIntlRequestCreateTestOrder request
    );

    /// <summary>
    ///     STAGE only. Creates a test order for the GULF regions (SA, AE, KW) and returns its order number. The SellerID
    ///     header must be the test seller; products must be approved.
    /// </summary>
    [Post("/integration/test/order/orders/local-gulf")]
    [Headers("SellerID: SELLER_ID_PARAMETER")]
    Task<IApiResponse<TrendyolResponseCreateTestOrder>> CreateIntlTestOrderGulfAsync(
        [Body] TrendyolIntlRequestCreateTestOrder request
    );

    /// <summary>
    ///     Creates an order webhook (createWebhook). Up to 15 per seller (passive ones count). Trendyol POSTs the full
    ///     package to the URL. Same as <see cref="ITrendyolWebhookApi.CreateWebhookAsync" /> plus the storefront
    ///     header and the optional <c>CountryCodes</c> filter.
    /// </summary>
    [Post("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks")]
    Task<IApiResponse<TrendyolResponseCreateWebhook>> CreateIntlWebhookAsync(
        [Body] TrendyolIntlRequestCreateWebhook request
    );

    /// <summary>Lists the webhooks (getWebhook), including their country filter.</summary>
    [Get("/integration/webhook/sellers/SELLER_ID_PARAMETER/webhooks")]
    Task<IApiResponse<List<TrendyolIntlWebhook>>> GetIntlWebhooksAsync();}
