using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Product Integration V2 services (content based). Operations that create, update, delete, archive, unlock or
///     change price/stock are queued: they return a batch request id to be followed with
///     <see cref="GetBatchRequestResultAsync" />.
/// </summary>
public interface ITrendyolProductApi
{
    /// <summary>
    ///     Lists Trendyol brands (GET /integration/product/brands). Use the returned id as brandId when creating products.
    ///     Pagination is required to get all brands.
    /// </summary>
    [Get("/integration/product/brands")]
    Task<IApiResponse<TrendyolResponseGetBrands>> GetBrandsAsync([Query] TrendyolFilterGetBrands filter);

    /// <summary>
    ///     Searches brands by name (GET /integration/product/brands/by-name). The search is case sensitive.
    /// </summary>
    [Get("/integration/product/brands/by-name")]
    Task<IApiResponse<List<TrendyolBrand>>> GetBrandsByNameAsync([Query] string name);

    /// <summary>
    ///     Creates a brand that is not in the Trendyol brand list. Sent as multipart/form-data with a
    ///     <c>name</c> text part and 1 to 3 <c>images</c> file parts.
    /// </summary>
    /// <param name="storeFrontCode">Country code the brand is created for, e.g. TR.</param>
    /// <param name="name">Brand name.</param>
    /// <param name="images">Brand images, minimum 1 and maximum 3.</param>
    [Multipart]
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/brands")]
    Task<IApiResponse<TrendyolResponseCreateBrand>> CreateBrandAsync(
        [Header("storeFrontCode")] string storeFrontCode,
        [AliasAs("name")] string name,
        [AliasAs("images")] IEnumerable<StreamPart> images);

    /// <summary>
    ///     Gets the category tree (GET /integration/product/product-categories). Products must use a leaf category id.
    ///     The tree changes periodically; refreshing it weekly is recommended.
    /// </summary>
    /// <param name="name">Returns categories (at any level) whose name contains this word.</param>
    /// <param name="storefrontCode">Country code; TR when omitted.</param>
    /// <param name="acceptLanguage">Response language for non-TR storefronts: tr, en, ro, ar or el.</param>
    [Get("/integration/product/product-categories")]
    Task<IApiResponse<TrendyolResponseGetCategories>> GetCategoriesAsync(
        [Query] string name = null,
        [Header("storefrontcode")] string storefrontCode = null,
        [Header("Accept-Language")] string acceptLanguage = null);

    /// <summary>
    ///     Gets the attributes of a leaf category (GET /integration/product/categories/{categoryId}/attributes).
    /// </summary>
    /// <param name="categoryId">Leaf category id.</param>
    /// <param name="required">true returns only required attributes, false only optional ones, null all of them.</param>
    /// <param name="storefrontCode">Country code; TR when omitted.</param>
    /// <param name="acceptLanguage">Response language for non-TR storefronts: tr, en, ro, ar or el.</param>
    [Get("/integration/product/categories/{categoryId}/attributes")]
    Task<IApiResponse<TrendyolResponseGetCategoryAttributes>> GetCategoryAttributesAsync(
        [AliasAs("categoryId")] long categoryId,
        [Query] bool? required = null,
        [Header("storefrontcode")] string storefrontCode = null,
        [Header("Accept-Language")] string acceptLanguage = null);

    /// <summary>
    ///     Gets the allowed values of a category attribute
    ///     (GET /integration/product/categories/{categoryId}/attributes/{attributeId}/values).
    /// </summary>
    [Get("/integration/product/categories/{categoryId}/attributes/{attributeId}/values")]
    Task<IApiResponse<TrendyolResponseGetCategoryAttributeValues>> GetCategoryAttributeValuesAsync(
        [AliasAs("categoryId")] long categoryId,
        [AliasAs("attributeId")] long attributeId,
        [Query] TrendyolFilterGetCategoryAttributeValues filter,
        [Header("storefrontcode")] string storefrontCode = null,
        [Header("Accept-Language")] string acceptLanguage = null);

    /// <summary>
    ///     Creates products (POST /integration/product/sellers/{sellerId}/v2/products). Up to 1000 items per request.
    ///     Items sharing a productMainId become variants of one product; listPrice must not be lower than salePrice.
    ///     Created products go through approval.
    /// </summary>
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/v2/products")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> CreateProductsAsync([Body] TrendyolRequestCreateProduct request);

    /// <summary>
    ///     Updates products that are not approved yet (rejected or pending). Up to 1000 items per request. All fields
    ///     required for creation must be sent; a field sent as null is cleared, so omit fields you do not want to change.
    /// </summary>
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/products/unapproved-bulk-update")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> UpdateUnapprovedProductsAsync([Body] TrendyolRequestUpdateUnapprovedProducts request);

    /// <summary>
    ///     Updates the content (title, description, images, attributes) of approved products by contentId. Up to 1000 items.
    ///     Partial update works except for attributes, where all attributes must be sent. The result of each update
    ///     can be checked with <see cref="GetUpdateAuditsAsync" />.
    /// </summary>
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/products/content-bulk-update")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> UpdateContentBulkAsync([Body] TrendyolRequestUpdateContentBulk request);

    /// <summary>
    ///     Updates variant data (stock code, VAT, addresses, desi, lot number, channels, ...) of approved products
    ///     by barcode. Up to 1000 items; partial update is supported.
    /// </summary>
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/products/variant-bulk-update")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> UpdateVariantBulkAsync([Body] TrendyolRequestUpdateVariantBulk request);

    /// <summary>
    ///     Updates the delivery duration of approved products by barcode. Up to 1000 items.
    ///     deliveryDuration 0 means ships today, 1 means ships by tomorrow at the latest.
    /// </summary>
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/products/delivery-info-bulk-update")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> UpdateDeliveryInfoBulkAsync([Body] TrendyolRequestUpdateDeliveryInfoBulk request);

    /// <summary>
    ///     Gets the outcome of the review step for content updates of a content
    ///     (GET /integration/product/sellers/{sellerId}/products/{contentId}/update-audits).
    /// </summary>
    /// <param name="contentId">Content id of the product.</param>
    /// <param name="filter">Paging and status (SUCCESS, FAIL, RUNNING) filter.</param>
    /// <param name="storefrontCode">Country code; TR when omitted.</param>
    /// <param name="acceptLanguage">Pass tr to get reject reasons in Turkish.</param>
    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/products/{contentId}/update-audits")]
    Task<IApiResponse<TrendyolResponseGetUpdateAudits>> GetUpdateAuditsAsync(
        [AliasAs("contentId")] long contentId,
        [Query] TrendyolFilterGetUpdateAudits filter,
        [Header("storefrontcode")] string storefrontCode = null,
        [Header("Accept-Language")] string acceptLanguage = null);

    /// <summary>
    ///     Updates stock and/or price of approved products (POST /integration/inventory/sellers/{sellerId}/products/price-and-inventory).
    ///     Up to 1000 items, up to 20000 stock per product. Sending an identical request again within 15 minutes is rejected.
    ///     The batch status is not returned for this operation; check the item statuses.
    /// </summary>
    [Post("/integration/inventory/sellers/SELLER_ID_PARAMETER/products/price-and-inventory")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> UpdatePriceAndInventoryAsync([Body] TrendyolRequestUpdatePriceAndInventory request);

    /// <summary>
    ///     Deletes products. Pending approval products and approved products that have been archived for more than a day
    ///     (and are not stopped by Trendyol) can be deleted.
    /// </summary>
    [Delete("/integration/product/sellers/SELLER_ID_PARAMETER/products")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> DeleteProductsAsync([Body] TrendyolRequestDeleteProduct request);

    /// <summary>
    ///     Archives or restores products (PUT /integration/product/sellers/{sellerId}/products/archive-state). Up to 1000 items.
    /// </summary>
    [Put("/integration/product/sellers/SELLER_ID_PARAMETER/products/archive-state")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> ArchiveProductsAsync([Body] TrendyolRequestArchiveProducts request);

    /// <summary>
    ///     Unlocks products that were locked because of low/high pricing, critical price errors or supply failures.
    /// </summary>
    [Put("/integration/product/sellers/SELLER_ID_PARAMETER/products/unlock")]
    Task<IApiResponse<TrendyolResponseBatchRequest>> UnlockProductsAsync([Body] TrendyolRequestUnlockProducts request);

    /// <summary>
    ///     Gets buybox order and prices for up to 10 barcodes. Rate limit 1000 requests per minute.
    /// </summary>
    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/products/buybox-information")]
    Task<IApiResponse<TrendyolResponseGetBuyboxInformation>> GetBuyboxInformationAsync([Body] TrendyolRequestGetBuyboxInformation request);

    /// <summary>
    ///     Gets the result of a queued operation. Results are kept for 4 hours.
    /// </summary>
    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/products/batch-requests/{batchRequestId}")]
    Task<IApiResponse<TrendyolResponseGetBatchRequest>> GetBatchRequestResultAsync([AliasAs("batchRequestId")] string batchRequestId);

    /// <summary>
    ///     Gets basic status information of a product by barcode.
    /// </summary>
    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/product/{barcode}")]
    Task<IApiResponse<TrendyolResponseGetProductBase>> GetProductBaseAsync([AliasAs("barcode")] string barcode);

    /// <summary>
    ///     Lists products that are rejected or pending approval. page x size is capped at 10000; use nextPageToken beyond that.
    /// </summary>
    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/products/unapproved")]
    Task<IApiResponse<TrendyolResponseGetUnapprovedProducts>> GetUnapprovedProductsAsync([Query(CollectionFormat.Multi)] TrendyolFilterGetUnapprovedProducts filter);

    /// <summary>
    ///     Lists approved products grouped by content (each content has its variants). Maximum page size is 100;
    ///     page x size is capped at 10000, use nextPageToken beyond that.
    /// </summary>
    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/products/approved")]
    Task<IApiResponse<TrendyolResponseGetApprovedProducts>> GetApprovedProductsAsync([Query(CollectionFormat.Multi)] TrendyolFilterGetApprovedProducts filter);

    /// <summary>
    ///     Lists only stock and price of approved products, grouped by content.
    /// </summary>
    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/products/approved/inventory-and-price")]
    Task<IApiResponse<TrendyolResponseGetApprovedProductsInventoryAndPrice>> GetApprovedProductsInventoryAndPriceAsync([Query(CollectionFormat.Multi)] TrendyolFilterGetApprovedProductsInventoryAndPrice filter);

    /// <summary>
    ///     Lists cargo providers whose codes can be sent in the cargoProviders field of product requests.
    /// </summary>
    /// <param name="storefrontCode">Country code; TR when omitted.</param>
    [Get("/integration/product/lookup/cargo-providers")]
    Task<IApiResponse<List<TrendyolCargoProvider>>> GetCargoProvidersAsync([Header("storefrontcode")] string storefrontCode = null);

    /// <summary>
    ///     Creates a product video from a URL (POST /integration/video/sellers/{sellerId}/videos). Rate limit 200 requests per minute.
    ///     A content can have one active video; processing continues in the background, follow it with <see cref="GetVideosAsync" />.
    /// </summary>
    [Post("/integration/video/sellers/SELLER_ID_PARAMETER/videos")]
    Task<IApiResponse<TrendyolResponseCreateVideo>> CreateVideoAsync([Body] TrendyolRequestCreateVideo request);

    /// <summary>
    ///     Lists or queries the seller's videos. Rate limit 1000 requests per minute.
    /// </summary>
    [Get("/integration/video/sellers/SELLER_ID_PARAMETER/videos")]
    Task<IApiResponse<TrendyolResponseGetVideos>> GetVideosAsync([Query] TrendyolFilterGetVideos filter);

}
