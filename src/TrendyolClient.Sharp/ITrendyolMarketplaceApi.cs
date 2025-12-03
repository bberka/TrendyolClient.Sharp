using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

public interface ITrendyolProductApi
{
    [Get("/integration/sellers/SELLER_ID_PARAMETER/addresses")]
    Task<IApiResponse<TrendyolResponseGetAddresses>> GetAddressesAsync();

    [Get("/integration/product/brands")]
    Task<IApiResponse<TrendyolResponseGetBrands>> GetBrandsAsync();

    [Get("/integration/product/product-categories")]
    Task<IApiResponse<TrendyolResponseGetCategories>> GetCategoriesAsync();

    [Get("/integration/product/product-categories/{categoryId}/attributes")]
    Task<IApiResponse<TrendyolResponseGetCategoryAttributes>> GetCategoryAttributesAsync([AliasAs("categoryId")] long categoryId);

    [Post("/integration/product/sellers/SELLER_ID_PARAMETER/products")]
    Task<IApiResponse<TrendyolResponseCreateProduct>> CreateProductsAsync([Body] TrendyolRequestCreateProduct request);

    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/products")]
    Task<IApiResponse<TrendyolResponseGetProducts>> GetProductsAsync([Query] TrendyolFilterGetProducts filter);

    [Get("/integration/product/sellers/SELLER_ID_PARAMETER/products/batch-requests/{batchRequestId}")]
    Task<IApiResponse<TrendyolResponseGetBatchRequest>> GetBatchRequestAsync([AliasAs("batchRequestId")] string batchRequestId);

    [Post("/integration/inventory/sellers/SELLER_ID_PARAMETER/products/price-and-inventory")]
    Task<IApiResponse<TrendyolResponseUpdatePriceAndInventory>> UpdatePriceAndInventoryAsync([Body] TrendyolRequestUpdatePriceAndInventory request);

    [Put("/integration/product/sellers/SELLER_ID_PARAMETER/products")]
    Task<IApiResponse<TrendyolResponseUpdateProduct>> UpdateProductsAsync([Body] TrendyolRequestUpdateProduct request);

    [Delete("/integration/product/sellers/SELLER_ID_PARAMETER/products")]
    Task<IApiResponse<TrendyolResponseDeleteProduct>> DeleteProductsAsync([Body] TrendyolRequestDeleteProduct request);
}

public interface ITrendyolOrderApi
{
    [Post("/integration/test/order/orders/core")]
    [Headers("SellerID: SELLER_ID_PARAMETER")]
    Task<IApiResponse<TrendyolResponseCreateTestOrder>> CreateTestOrderAsync([Body] TrendyolRequestCreateTestOrder request);

    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/orders")]
    Task<IApiResponse<TrendyolResponseGetOrderPackages>> GetOrderPackagesAsync([Query] TrendyolFilterGetOrderPackages filter);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/update-tracking-number")]
    Task<IApiResponse> UpdateTrackingNumberAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestUpdateTrackingNumber request);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}")]
    Task<IApiResponse> NotifyPackageStatusAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestNotifyPackageStatus request);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/items/unsupplied")]
    Task<IApiResponse> CancelOrderPackageItemAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestCancelOrderPackageItem request);

    [Post("/integration/sellers/SELLER_ID_PARAMETER/seller-invoice-links")]
    Task<IApiResponse> SendInvoiceLinkAsync([Body] TrendyolRequestSendInvoiceLink request);

    [Post("/integration/sellers/SELLER_ID_PARAMETER/seller-invoice-links/delete")]
    Task<IApiResponse> DeleteInvoiceLinkAsync([Body] TrendyolRequestDeleteInvoiceLink request);

    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/split-packages")]
    Task<IApiResponse<TrendyolResponseSplitPackage>> SplitMultiPackageByQuantityAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestSplitMultiPackageByQuantity request
    );

    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/split")]
    Task<IApiResponse<TrendyolResponseSplitPackage>> SplitShipmentPackageAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestSplitShipmentPackage request
    );

    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/multi-split")]
    Task<IApiResponse<TrendyolResponseSplitPackage>> MultiSplitShipmentPackageAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestMultiSplitShipmentPackage request
    );

    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/quantity-split")]
    Task<IApiResponse<TrendyolResponseSplitPackage>> SplitShipmentPackageByQuantityAsync(
        [AliasAs("packageId")] long packageId,
        [Body] TrendyolRequestSplitShipmentPackageByQuantity request
    );

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/box-info")]
    Task<IApiResponse> NotifyBoxInfoAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestNotifyBoxInfo request);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/delivered-by-service")]
    Task<IApiResponse> DeliveryByServiceAsync([AliasAs("packageId")] long packageId);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/cargo-providers")]
    Task<IApiResponse> ChangeCargoProviderAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestChangeCargoProvider request);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/warehouse")]
    Task<IApiResponse> UpdateWarehouseAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestUpdateWarehouse request);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/extended-agreed-delivery-date")]
    Task<IApiResponse> ExtendAgreedDeliveryDateAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestExtendAgreedDeliveryDate request);

    [Put("/integration/test/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/status")]
    Task<IApiResponse> UpdateTestOrderStatusAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestUpdateTestOrderStatus request);

    [Put("/integration/test/order/sellers/SELLER_ID_PARAMETER/claims/waiting-in-action")]
    Task<IApiResponse> ReturnTestOrderToWaitingInActionAsync([Body] TrendyolRequestReturnTestOrder request);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/alternative-delivery")]
    Task<IApiResponse> AlternativeDeliveryAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestAlternativeDelivery request);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/manual-deliver/{cargoTrackingNumber}")]
    Task<IApiResponse> ManualDeliverAsync([AliasAs("cargoTrackingNumber")] string cargoTrackingNumber);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/manual-return/{cargoTrackingNumber}")]
    Task<IApiResponse> ManualReturnAsync([AliasAs("cargoTrackingNumber")] string cargoTrackingNumber);

    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/shipment-packages/{packageId}/labor-costs")]
    Task<IApiResponse> UpdateLaborCostAsync([AliasAs("packageId")] long packageId, [Body] TrendyolRequestUpdateLaborCost request);
}

public interface ITrendyolCommonLabelApi
{
    [Post("/integration/sellers/SELLER_ID_PARAMETER/common-label/{cargoTrackingNumber}")]
    Task<IApiResponse<TrendyolResponseRequestBarcode>> RequestBarcodeAsync(
        [AliasAs("cargoTrackingNumber")] string cargoTrackingNumber,
        [Body] TrendyolRequestBarcode request
    );

    [Get("/integration/sellers/SELLER_ID_PARAMETER/common-label/{cargoTrackingNumber}")]
    Task<IApiResponse<TrendyolResponseGetCommonLabel>> GetCommonLabelAsync([AliasAs("cargoTrackingNumber")] string cargoTrackingNumber);
}

public interface ITrendyolClaimApi
{
    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims")]
    Task<IApiResponse<TrendyolResponseGetClaims>> GetClaimsAsync([Query] TrendyolFilterGetClaims filter);

    [Put("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/items/approve")]
    Task<IApiResponse> ApproveClaimAsync([AliasAs("claimId")] long claimId, [Body] TrendyolRequestApproveClaim request);

    [Post("/integration/order/sellers/SELLER_ID_PARAMETER/claims/{claimId}/issue")]
    Task<IApiResponse> CreateClaimIssueAsync(
        [AliasAs("claimId")] long claimId,
        [Query] long claimIssueReasonId,
        [Query] string claimItemIdList,
        [Query] string description
    );

    [Get("/integration/order/claim-issue-reasons")]
    Task<IApiResponse<TrendyolResponseGetClaimIssueReasons>> GetClaimIssueReasonsAsync();

    [Get("/integration/order/sellers/SELLER_ID_PARAMETER/claims/items/{claimItemsId}/audit")]
    Task<IApiResponse<TrendyolResponseGetClaimAudit>> GetClaimAuditAsync([AliasAs("claimItemsId")] string claimItemsId);
}

public interface ITrendyolQnAApi
{
    [Get("/integration/qna/sellers/SELLER_ID_PARAMETER/questions/filter")]
    Task<IApiResponse<TrendyolResponseGetQuestions>> GetQuestionsAsync([Query] TrendyolFilterGetQuestions filter);

    [Post("/integration/qna/sellers/SELLER_ID_PARAMETER/questions/{id}/answers")]
    Task<IApiResponse> AnswerQuestionAsync([AliasAs("id")] long id, [Body] TrendyolRequestAnswerQuestion request);
}

public interface ITrendyolMemberApi
{
    [Get("/integration/member/countries")]
    Task<IApiResponse<TrendyolResponseGetCountries>> GetCountriesAsync();

    [Get("/integration/member/countries/{countryCode}/cities")]
    Task<IApiResponse<TrendyolResponseGetCities>> GetCitiesAsync([AliasAs("countryCode")] string countryCode);

    [Get("/integration/member/countries/domestic/{countryCode}/cities")]
    Task<IApiResponse<TrendyolResponseGetCities>> GetDomesticCitiesAsync([AliasAs("countryCode")] string countryCode);
}

public interface ITrendyolMarketplaceApi : ITrendyolProductApi, ITrendyolOrderApi, ITrendyolCommonLabelApi, ITrendyolClaimApi, ITrendyolQnAApi,
    ITrendyolMemberApi
{
}