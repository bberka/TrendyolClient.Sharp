using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Order (V2) integration endpoints: shipment packages, package operations and stage-only test order helpers.
///     https://developers.trendyol.com/docs/sipariş-paketlerini-çekme-getshipmentpackages
/// </summary>
public interface ITrendyolOrderApi
{
    /// <summary>
    ///     Lists shipment packages (getShipmentPackages, Order V2). Up to 1000 requests per minute.
    ///     Only the last month is queryable, a date range may span at most two weeks (the last week is returned
    ///     without dates), <c>size</c> is at most 200 and at most 10,000 packages (pages 0-49 at size 200) are reachable.
    ///     Use <see cref="GetOrderPackagesStreamAsync" /> for full scans and periodic synchronisation.
    /// </summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/v2/orders")]
    Task<IApiResponse<TrendyolResponseGetOrderPackages>> GetOrderPackagesAsync([Query] TrendyolFilterGetOrderPackages filter);

    /// <summary>
    ///     Streams shipment packages with cursor pagination (getShipmentPackagesStream), newest modification first.
    ///     Covers the last three months, a window of at most 14 days (the last two weeks by default).
    ///     Pass <c>NextCursor</c> from each response until <c>HasMore</c> is false; keep the filters unchanged
    ///     while paging. Call at most once every 5 seconds.
    /// </summary>
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/orders/stream")]
    Task<IApiResponse<TrendyolResponseGetOrderPackagesStream>> GetOrderPackagesStreamAsync(
        [Query] TrendyolFilterGetOrderPackagesStream filter
    );

    /// <summary>
    ///     Updates a package status (updatePackage). Only "Picking" and "Invoiced" can be sent, in that order;
    ///     "Invoiced" takes <c>params.invoiceNumber</c>.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}")]
    Task<IApiResponse> NotifyPackageStatusAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestNotifyPackageStatus request);

    /// <summary>
    ///     Reports unsupplied items (updatePackage). The package is broken up and a new package with a new
    ///     id and cargo tracking number is created under the same order number, so fetch packages again afterwards.
    ///     Reason ids: 500 out of stock, 501 defective, 502 wrong price, 504 integration error,
    ///     505 bulk purchase, 506 force majeure.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/items/unsupplied")]
    Task<IApiResponse> CancelOrderPackageItemAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestCancelOrderPackageItem request);

    /// <summary>
    ///     Sets box count and deci for a package (updateBoxInfo). Required for Horoz and CEVA Logistics packages.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/box-info")]
    Task<IApiResponse> NotifyBoxInfoAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestNotifyBoxInfo request);

    /// <summary>
    ///     Marks a package as sent to an authorised service (deliveredByService). For sellers on the supplier-pays
    ///     model with logistics firms (currently Horoz). Can be called any time before the package is shipped.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/delivered-by-service")]
    Task<IApiResponse> DeliveryByServiceAsync([AliasAs("packageId")] long packageId);

    /// <summary>
    ///     Changes the cargo provider of a package (changeCargoProvider). Once per package every 5 minutes;
    ///     TEX requests fail when the seller's TEX quota is full. Fetch the package again afterwards.
    ///     Codes: YKMP, ARASMP, SURATMP, HOROZMP, DHLECOMMP, PTTMP, CEVAMP, TEXMP, KOLAYGELSINMP, CEVATEDARIK.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/cargo-providers")]
    Task<IApiResponse> ChangeCargoProviderAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestChangeCargoProvider request);

    /// <summary>
    ///     Gives a package extra supply time (extendAgreedDeliveryDate): extends the agreed delivery date by 1, 2 or 3
    ///     days. Documented only in the v6.0 doc set (Turkish, "Ek Tedarik Suresi Tanimlama"), not in the Turkiye
    ///     v1/v2 or International v3 docs, so it applies to the Turkiye marketplace integration. The 02.04.2026
    ///     changelog removed the agreedDeliveryDateExtendible, extendedAgreedDeliveryDate and
    ///     agreedDeliveryExtension(Start|End)Date fields from the package responses, so the response no longer tells
    ///     whether a package can be extended.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/extended-agreed-delivery-date")]
    Task<IApiResponse> ExtendAgreedDeliveryDateAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestExtendAgreedDeliveryDate request);

    /// <summary>
    ///     Updates the warehouse of a package (updateWarehouse). Trendyol Express sellers only; the package must be
    ///     Created, Picking or Invoiced.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/warehouse")]
    Task<IApiResponse> UpdateWarehouseAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestUpdateWarehouse request);

    /// <summary>
    ///     Reports the labor cost of an order line (laborCosts), for categories where product price and
    ///     labor cost must be invoiced separately (Turkey marketplace).
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/labor-costs")]
    Task<IApiResponse> UpdateLaborCostAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestUpdateLaborCost request);

    /// <summary>
    ///     Splits a package into several packages by quantity per barcode (splitMultiPackageByQuantity).
    ///     Lines left out go into a separate new package. New packages are created asynchronously in "UnPacked"
    ///     status, so fetch the packages again afterwards.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/split-packages")]
    Task<IApiResponse> SplitMultiPackageByQuantityAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestSplitMultiPackageByQuantity request
    );

    /// <summary>
    ///     Moves the given order lines into one new package (splitShipmentPackage).
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/split")]
    Task<IApiResponse<TrendyolResponseSplitShipmentPackage>> SplitShipmentPackageAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestSplitShipmentPackage request
    );

    /// <summary>
    ///     Splits a package into several packages in one request (multi-split). Do not send every line of the
    ///     package; the remaining lines get their own package automatically.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/multi-split")]
    Task<IApiResponse<TrendyolResponseMultiSplitShipmentPackage>> MultiSplitShipmentPackageAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestMultiSplitShipmentPackage request
    );

    /// <summary>
    ///     Splits the quantity of order lines into several packages (splitShipmentPackageByQuantity).
    ///     <c>quantities [2,2]</c> on a line of 4 creates two packages of 2.
    /// </summary>
    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/quantity-split")]
    Task<IApiResponse<TrendyolResponseSplitPackage>> SplitShipmentPackageByQuantityAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestSplitShipmentPackageByQuantity request
    );

    /// <summary>
    ///     Ships a package with an alternative delivery (processAlternativeDelivery): a cargo tracking link
    ///     (<c>IsPhoneNumber</c> false) or a contact phone number (<c>IsPhoneNumber</c> true, optional box/deci).
    ///     The package moves to "Shipped".
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/alternative-delivery")]
    Task<IApiResponse> AlternativeDeliveryAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestAlternativeDelivery request);

    /// <summary>
    ///     Delivers a digital product code (processAlternativeDelivery) to the customer by SMS and e-mail.
    ///     Only for packages whose line businessUnit is "Digital Goods"; otherwise Trendyol answers
    ///     "digital.good.business.unit.not.valid".
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/alternative-delivery")]
    Task<IApiResponse> AlternativeDeliveryDigitalAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestAlternativeDeliveryDigital request
    );

    /// <summary>
    ///     Marks an alternatively shipped package delivered by package id (manualDeliver). The customer is asked
    ///     to confirm; the package becomes "Delivered" on confirmation or after 3 business days.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/manual-deliver")]
    Task<IApiResponse> ManualDeliverByPackageIdAsync([AliasAs("packageId")] long packageId);

    /// <summary>
    ///     Same as <see cref="ManualDeliverByPackageIdAsync" />, addressed by cargo tracking number.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/manual-deliver/{cargoTrackingNumber}")]
    Task<IApiResponse> ManualDeliverAsync([AliasAs("cargoTrackingNumber")] string cargoTrackingNumber);

    /// <summary>
    ///     Returns an alternatively shipped package that could not be delivered and came back to the warehouse
    ///     (manualReturn), addressed by package id.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/manual-return")]
    Task<IApiResponse> ManualReturnByPackageIdAsync([AliasAs("packageId")] long packageId);

    /// <summary>
    ///     Same as <see cref="ManualReturnByPackageIdAsync" />, addressed by cargo tracking number.
    /// </summary>
    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/manual-return/{cargoTrackingNumber}")]
    Task<IApiResponse> ManualReturnAsync([AliasAs("cargoTrackingNumber")] string cargoTrackingNumber);

    /// <summary>
    ///     STAGE only. Creates a test order and returns its order number. The SellerID header must be the
    ///     test seller. Set <c>Commercial</c> for corporate invoices and <c>MicroRegion</c> ("AZ" or "GULF") for micro export.
    /// </summary>
    [Post("/integration/test/order/orders/core")]
    [Headers("SellerID: SELLER_ID_PARAMETER")]
    Task<IApiResponse<TrendyolResponseCreateTestOrder>> CreateTestOrderAsync([Body] TrendyolRequestCreateTestOrder request);

    /// <summary>
    ///     STAGE only. Moves a test package forward to Shipped, AtCollectionPoint, Delivered, UnDelivered or
    ///     Returned. Statuses only progress in that order.
    /// </summary>
    [Put("/integration/test/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/status")]
    [Headers("SellerID: SELLER_ID_PARAMETER")]
    Task<IApiResponse> UpdateTestOrderStatusAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestUpdateTestOrderStatus request);

    /// <summary>
    ///     STAGE only. Moves the claim of a test order to WaitingInAction. Use the <c>orderShipmentPackageId</c>
    ///     returned by getClaims as <c>ShipmentPackageId</c>.
    /// </summary>
    [Put("/integration/test/order/sellers/SELLER_ID_PARAMETER/claims/waiting-in-action")]
    Task<IApiResponse> ReturnTestOrderToWaitingInActionAsync([Body] TrendyolRequestReturnTestOrder request);
}
