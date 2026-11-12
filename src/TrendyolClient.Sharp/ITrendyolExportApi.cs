using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Trendyol Export Center (Ihracat Merkezi / AutoFT) integration. Trendyol buys the seller's marketplace products
///     and sells them on its overseas platforms.
///     https://developers.trendyol.com/reference/autoft-create-products-v2
///     The service lives under /integration/ecgw and puts the seller ID directly after the version segment
///     (not under /sellers/), so the SELLER_ID_PARAMETER token is used in that position.
///     Every call requires three headers: x-clientip (IPv4 the request is sent from), x-correlationid (UUID used for
///     log tracking) and x-agentname (name of the client/integrator).
/// </summary>
public interface ITrendyolExportApi
{
    /// <summary>
    ///     Opens approved marketplace products (max 5000 per request) for export. Products must have been created in the
    ///     seller panel. A barcode's price can be updated once per day. Returns a batch ID for the batch status service.
    /// </summary>
    [Post("/integration/ecgw/v2/SELLER_ID_PARAMETER/products")]
    Task<IApiResponse<TrendyolExportResponseBatch>> CreateProductsAsync(
        [Body] TrendyolExportRequestCreateProducts request,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Lists products opened for export. Paging is key based: send no PageKey on the first call, then the
    ///     x-paging-key header of the previous response (available through <c>IApiResponse.Headers</c>).
    /// </summary>
    [Get("/integration/ecgw/v2/SELLER_ID_PARAMETER/products")]
    Task<IApiResponse<List<TrendyolExportProduct>>> GetProductsAsync(
        [Query] TrendyolExportFilterGetProducts filter,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the origin values accepted by <see cref="CreateProductsAsync" />.
    /// </summary>
    [Get("/integration/ecgw/v1/SELLER_ID_PARAMETER/lookup/origins")]
    Task<IApiResponse<TrendyolExportResponseGetLookupItems>> GetOriginsAsync(
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the material composition values accepted by <see cref="CreateProductsAsync" />.
    /// </summary>
    [Get("/integration/ecgw/v1/SELLER_ID_PARAMETER/lookup/compositions")]
    Task<IApiResponse<TrendyolExportResponseGetLookupItems>> GetCompositionsAsync(
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the care (washing) instruction codes.
    /// </summary>
    [Get("/integration/ecgw/v1/SELLER_ID_PARAMETER/lookup/care-instructions")]
    Task<IApiResponse<TrendyolExportResponseGetCareInstructions>> GetCareInstructionsAsync(
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the required attributes of a category for <see cref="CreateProductsAsync" />.
    /// </summary>
    /// <param name="categoryId">Category ID</param>
    [Get("/integration/ecgw/v1/SELLER_ID_PARAMETER/lookup/product-categories/{categoryId}/attributes")]
    Task<IApiResponse<TrendyolExportResponseGetCategoryAttributes>> GetCategoryAttributesAsync(
        [AliasAs("categoryId")] long categoryId,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the category of each barcode, needed to look up the required attributes.
    /// </summary>
    [Post("/integration/ecgw/v1/SELLER_ID_PARAMETER/lookup/product-categories/by-barcodes")]
    Task<IApiResponse<TrendyolExportResponseGetCategoriesByBarcodes>> GetCategoriesByBarcodesAsync(
        [Body] TrendyolExportRequestGetCategoriesByBarcodes request,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Updates stock of products already opened for export (max 5000 per request). Not available to sellers whose
    ///     stock is managed jointly with the seller panel. Check the returned batch ID for the result.
    /// </summary>
    [Post("/integration/ecgw/v1/SELLER_ID_PARAMETER/stocks")]
    Task<IApiResponse<TrendyolExportResponseBatch>> UpdateStocksAsync(
        [Body] TrendyolExportRequestUpdateStocks request,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Updates prices of products already opened for export (max 5000 per request). The buying price is the VAT
    ///     inclusive price Trendyol buys at. A barcode's price can be updated once per day.
    /// </summary>
    [Post("/integration/ecgw/v1/SELLER_ID_PARAMETER/prices")]
    Task<IApiResponse<TrendyolExportResponseBatch>> UpdatePricesAsync(
        [Body] TrendyolExportRequestUpdatePrices request,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Lists packages. A new package is created daily between 08:00 and 08:15 when there are orders. Use
    ///     <see cref="GetPackageItemsV2Async" /> for the items awaiting supply in a package.
    /// </summary>
    [Get("/integration/ecgw/v2/SELLER_ID_PARAMETER/packages")]
    Task<IApiResponse<TrendyolExportResponseGetPackagesV2>> GetPackagesV2Async(
        [Query] TrendyolExportFilterGetPackagesV2 filter,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the items of a package, including packages that do not exist yet (status new) to plan stock.
    ///     Cancelling a new item lowers newQuantity only; cancelling a pending item lowers pendingQuantity and raises cancelledQuantity.
    /// </summary>
    [Get("/integration/ecgw/v2/SELLER_ID_PARAMETER/packages/items")]
    Task<IApiResponse<TrendyolExportResponseGetPackageItemsV2>> GetPackageItemsV2Async(
        [Query] TrendyolExportFilterGetPackageItemsV2 filter,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Lists package items in a single call (combines the package list and package detail services).
    /// </summary>
    [Get("/integration/ecgw/v3/SELLER_ID_PARAMETER/packages")]
    Task<IApiResponse<TrendyolExportResponseGetPackagesV3>> GetPackagesV3Async(
        [Query] TrendyolExportFilterGetPackagesV3 filter,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the result of a product create, stock update or price update batch. Batch IDs are deleted after 24 hours.
    /// </summary>
    /// <param name="batchId">Batch ID (UUID) returned by the operation</param>
    [Get("/integration/ecgw/v1/SELLER_ID_PARAMETER/check-status")]
    Task<IApiResponse<TrendyolExportResponseGetBatchStatus>> GetBatchStatusAsync(
        [Query] string batchId,
        [Header("x-clientip")] string clientIp,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );
}
