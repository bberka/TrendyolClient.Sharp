using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Models.Marketplace;
using TrendyolClient.Sharp.Models.Marketplace.Filter;
using TrendyolClient.Sharp.Models.Marketplace.Request;
using TrendyolClient.Sharp.Models.Marketplace.Response;

namespace TrendyolClient.Sharp;

//TODO: IMPL https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/test-siparisi-olusturma
//TODO: IMPL https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/test-siparisi-stat%C3%BC-update

public interface ITrendyolMarketplaceApi
{
  /// <summary>
  /// Creates a common label for a given cargo tracking number.
  /// https://developers.trendyol.com/docs/marketplace/ortak-etiket-entegrasyonu/barkod-talebi
  /// </summary>
  /// <param name="cargoTrackingNumber">Tracking number for the package</param>
  /// <param name="request">Request body with label details</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/sellers/{SID}/common-label/{cargoTrackingNumber}")]
  Task<TrendyolApiResult> CreateCommonLabelAsync(
    [AliasAs("cargoTrackingNumber")] string cargoTrackingNumber,
    [Body] RequestCreateCommonLabel request
  );

  /// <summary>
  /// Retrieves a common label for a given cargo tracking number.
  /// https://developers.trendyol.com/docs/category/ortak-etiket-entegrasyonu
  /// </summary>
  /// <param name="cargoTrackingNumber">Tracking number for the package</param>
  /// <returns>Trendyol API response</returns>
  [Get("/integration/sellers/{SID}/common-label/{cargoTrackingNumber}")]
  Task<TrendyolApiResult> GetCommonLabelAsync(
    [AliasAs("cargoTrackingNumber")] string cargoTrackingNumber
  );

  /// <summary>
  /// Retrieves shipment packages based on filters.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-cekme
  /// </summary>
  /// <param name="filter">Filter parameters for shipment package retrieval</param>
  /// <returns>Trendyol API response containing shipment packages</returns>
  [Get("/integration/order/sellers/{SID}/orders")]
  Task<TrendyolApiResult<ResponseGetShipmentPackages>> GetShipmentPackagesAsync(
    [Query] FilterGetShipmentPackages filter
  );

  /// <summary>
  /// Updates the tracking number for a given shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/kargo-takip-kodu-bildirme
  /// </summary>
  /// <param name="shipmentPackageId">Shipment package ID</param>
  /// <param name="request">Request body containing the new tracking number</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{shipmentPackageId}/update-tracking-number")]
  Task<TrendyolApiResult> UpdateTrackingNumberAsync(
    [AliasAs("shipmentPackageId")] string shipmentPackageId,
    [Body] RequestUpdateTrackingNumber request
  );

  /// <summary>
  /// Updates the status of a shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/paket-statu-bildirimi
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing the new package status</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}")]
  Task<TrendyolApiResult> UpdatePackageStatusAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestUpdatePackageStatus request
  );

  /// <summary>
  /// Marks items in a package as unsupplied.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/tedarik-edememe-bildirimi
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing details of unsupplied items</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}/items/unsupplied")]
  Task<TrendyolApiResult> UpdatePackageUnsuppliedAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestUpdatePackageUnsupplied request
  );

  /// <summary>
  /// Sends an invoice link for a seller.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-gonderme
  /// </summary>
  /// <param name="request">Request body containing invoice link details</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/sellers/{SID}/seller-invoice-links")]
  Task<TrendyolApiResult> SendInvoiceLinkAsync(
    [Body] RequestSendInvoiceLink request
  );

  /// <summary>
  /// Deletes an invoice link that was fed incorrectly.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/fatura-linki-silme
  /// <br /><br />
  /// Invoices that were fed incorrectly before, can be deleted through this service and fed again through the send invoice link service.
  /// </summary>
  /// <param name="request">Request body containing details for invoice link deletion</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/sellers/{SID}/seller-invoice-links/delete")]
  Task<TrendyolApiResult> DeleteInvoiceLinkAsync(
    [Body] RequestDeleteInvoiceLink request
  );

  /// <summary>
  /// Splits a multi-package shipment by quantity.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing split details</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/order/sellers/{SID}/shipment-packages/{packageId}/split-packages")]
  Task<TrendyolApiResult> SplitMultiPackageByQuantityAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestSplitMultiPackageByQuantity request
  );

  /// <summary>
  /// Splits a shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/order/sellers/{SID}/shipment-packages/{packageId}/split")]
  Task<TrendyolApiResult> SplitShipmentPackageAsync(
    [AliasAs("packageId")] string packageId
  );

  /// <summary>
  /// Performs a multi-split operation on a shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/order/sellers/{SID}/shipment-packages/{packageId}/multi-split")]
  Task<TrendyolApiResult> MultiSplitShipmentPackageAsync(
    [AliasAs("packageId")] string packageId
  );

  /// <summary>
  /// Splits a shipment package by quantity.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/siparis-paketlerini-bolme
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/order/sellers/{SID}/shipment-packages/{packageId}/quantity-split")]
  Task<TrendyolApiResult> SplitShipmentPackageByQuantityAsync(
    [AliasAs("packageId")] string packageId
  );

  /// <summary>
  /// Updates box and desi (volume) information for a shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/desi-ve-koli-bilgisi-bildirimi
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing updated box info</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}/box-info")]
  Task<TrendyolApiResult> UpdateBoxInfoAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestUpdateBoxInfo request
  );

  /// <summary>
  /// Processes an alternative delivery for a given shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/alternatif-teslimat-ile-gonderim
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing alternative delivery details</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}/alternative-delivery")]
  Task<TrendyolApiResult> ProcessAlternativeDeliveryAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestProcessAlternativeDelivery request
  );

  /// <summary>
  /// Manually delivers a shipment package using a cargo tracking number.
  /// https://api.trendyol.com/sapigw/suppliers/{supplierId}/manual-deliver/{cargoTrackingNumber}
  /// </summary>
  /// <param name="cargoTrackingNumber">Cargo tracking number</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/manual-deliver/{cargoTrackingNumber}")]
  Task<TrendyolApiResult> ManualDeliverAsync(
    [AliasAs("cargoTrackingNumber")] string cargoTrackingNumber
  );

  /// <summary>
  /// Manually returns a shipment package using a cargo tracking number.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/alternatif-teslimat-ile-gonderim
  /// </summary>
  /// <param name="cargoTrackingNumber">Cargo tracking number</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/manual-return/{cargoTrackingNumber}")]
  Task<TrendyolApiResult> ManualReturnAsync(
    [AliasAs("cargoTrackingNumber")] string cargoTrackingNumber
  );

  /// <summary>
  /// Marks a shipment package as delivered by an authorized service.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/yetkili-servis-gonderimi
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}/delivered-by-service")]
  Task<TrendyolApiResult> DeliveredByServiceAsync(
    [AliasAs("packageId")] string packageId
  );

  /// <summary>
  /// Changes the cargo provider for a shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/paket-kargo-firmasi-degistirme
  /// Usable cargo providers: “YKMP”, “ARASMP”, “SURATMP”, “HOROZMP”, “MNGMP”, “PTTMP”, “CEVAMP”, “TEXMP”, "KOLAYGELSINMP"
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing new cargo provider details</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}/cargo-providers")]
  Task<TrendyolApiResult> ChangeCargoProviderAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestChangeCargoProvider request
  );

  /// <summary>
  /// Updates warehouse information for a shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/depo-bilgisi-guncelleme
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing updated warehouse info</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}/warehouse")]
  Task<TrendyolApiResult> UpdateWarehouseAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestUpdateWarehouse request
  );

  /// <summary>
  /// Extends the agreed delivery date for a shipment package.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/ek-tedarik-s%C3%BCresi-tan%C4%B1mlama
  /// </summary>
  /// <param name="packageId">Package ID</param>
  /// <param name="request">Request body containing extended delivery date info</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/shipment-packages/{packageId}/extended-agreed-delivery-date")]
  Task<TrendyolApiResult> AgreedDeliveryDateAsync(
    [AliasAs("packageId")] string packageId,
    [Body] RequestAgreedDeliveryDate request
  );

  /// <summary>
  /// Retrieves a list of countries.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
  /// </summary>
  /// <returns>Trendyol API response containing a list of countries</returns>
  [Get("/integration/member/countries")]
  Task<TrendyolApiResult> GetCountriesAsync();

  /// <summary>
  /// Retrieves a list of cities in the GULF region for a given country.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
  /// </summary>
  /// <param name="countryCode">Country code (ISO Alpha-2 format)</param>
  /// <returns>Trendyol API response containing a list of cities</returns>
  [Get("/integration/member/countries/{countryCode}/cities")]
  Task<TrendyolApiResult> GetCitiesGULFAsync(
    [AliasAs("countryCode")] string countryCode
  );

  /// <summary>
  /// Retrieves a list of cities in Azerbaijan.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
  /// </summary>
  /// <returns>Trendyol API response containing a list of cities in Azerbaijan</returns>
  [Get("/integration/member/countries/domestic/AZ/cities")]
  Task<TrendyolApiResult> GetCitiesAzerbaijanAsync();

  /// <summary>
  /// Retrieves a list of districts in Azerbaijan for a given city.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
  /// </summary>
  /// <param name="cityCode">City code</param>
  /// <returns>Trendyol API response containing a list of districts</returns>
  [Get("/integration/member/countries/domestic/AZ/cities/{cityCode}/districts")]
  Task<TrendyolApiResult> GetDistrictsAzerbaijanAsync(
    [AliasAs("cityCode")] string cityCode
  );

  /// <summary>
  /// Retrieves a list of cities in Turkey.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
  /// </summary>
  /// <returns>Trendyol API response containing a list of cities</returns>
  [Get("/integration/member/countries/domestic/TR/cities")]
  Task<TrendyolApiResult> GetCitiesTurkeyAsync();

  /// <summary>
  /// Retrieves a list of districts in Turkey for a given city.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
  /// </summary>
  /// <param name="cityCode">City code</param>
  /// <returns>Trendyol API response containing a list of districts</returns>
  [Get("/integration/member/countries/domestic/TR/cities/{cityCode}/districts")]
  Task<TrendyolApiResult> GetDistrictsTurkeyAsync(
    [AliasAs("cityCode")] string cityCode
  );

  /// <summary>
  /// Retrieves a list of neighborhoods in Turkey for a given city and district.
  /// https://developers.trendyol.com/docs/marketplace/siparis-entegrasyonu/adres-bilgisi
  /// </summary>
  /// <param name="cityCode">City code</param>
  /// <param name="districtCode">District code</param>
  /// <returns>Trendyol API response containing a list of neighborhoods</returns>
  [Get("/integration/member/countries/domestic/TR/cities/{cityCode}/districts/{districtCode}/neighborhoods")]
  Task<TrendyolApiResult> GetNeighborhoodsTurkeyAsync(
    [AliasAs("cityCode")] string cityCode,
    [AliasAs("districtCode")] string districtCode
  );

  /// <summary>
  /// Retrieves the supplier's return and shipment addresses.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/iade-ve-sevkiyat-adres-bilgileri
  /// </summary>
  /// <returns>Trendyol API response containing supplier addresses</returns>
  [Get("/integration/sellers/{SID}/addresses")]
  Task<TrendyolApiResult<ResponseGetSuppliersAddresses>> GetSupplierAddressesAsync();

  /// <summary>
  /// Retrieves the list of brands available on Trendyol.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  /// </summary>
  /// <param name="filter">Filter parameters for brand listing</param>
  /// <returns>Trendyol API response containing a list of brands</returns>
  [Get("/integration/product/brands")]
  Task<TrendyolApiResult<ResponseGetBrands>> GetBrandsAsync(
    [Query] FilterPage filter
  );

  /// <summary>
  /// Retrieves brand details by name.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  /// </summary>
  /// <param name="brandName">Brand name to search for</param>
  /// <returns>Trendyol API response containing brand details</returns>
  [Get("/integration/product/brands/by-name")]
  Task<TrendyolApiResult<ResponseGetBrandsByName>> GetBrandsByNameAsync(
    [Query] string brandName
  );

  /// <summary>
  /// Retrieves the category tree of Trendyol products.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-listesi
  /// </summary>
  /// <param name="filter">Filter parameters for category retrieval</param>
  /// <returns>Trendyol API response containing category tree</returns>
  [Get("/integration/product/product-categories")]
  Task<TrendyolApiResult<ResponseGetCategoryTree>> GetCategoryTreeAsync(
    [Query] FilterGetCategoryTree filter
  );

  /// <summary>
  /// Retrieves category attributes for a given category ID.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-ozellik-listesi
  /// </summary>
  /// <param name="categoryId">Category ID</param>
  /// <param name="filter">Filter parameters for category attributes</param>
  /// <returns>Trendyol API response containing category attributes</returns>
  [Get("/integration/product/product-categories/{categoryId}/attributes")]
  Task<TrendyolApiResult<ResponseGetCategoryAttributes>> GetCategoryAttributesAsync(
    [AliasAs("categoryId")] int categoryId,
    [Query] FilterGetCategoryAttributes filter
  );

  /// <summary>
  /// Creates new products in Trendyol.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-aktarma-v2
  /// </summary>
  /// <param name="request">Request body containing product details</param>
  /// <returns>Trendyol API response containing batch request ID</returns>
  [Post("/integration/product/sellers/{SID}/products")]
  Task<TrendyolApiResult<ResponseBatchRequestId>> CreateProductsAsync(
    [Body] RequestCreateProducts request
  );

  /// <summary>
  /// Updates existing products in Trendyol.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-urun-bilgisi-guncelleme
  /// </summary>
  /// <param name="request">Request body containing updated product details</param>
  /// <returns>Trendyol API response containing batch request ID</returns>
  [Put("/integration/product/sellers/{SID}/products")]
  Task<TrendyolApiResult<ResponseBatchRequestId>> UpdateProductsAsync(
    [Body] RequestUpdateProduct request
  );

  /// <summary>
  /// Updates price and inventory of products.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/stok-ve-fiyat-guncelleme
  /// </summary>
  /// <param name="request">Request body containing price and inventory details</param>
  /// <returns>Trendyol API response containing batch request ID</returns>
  [Post("/integration/inventory/sellers/{SID}/products/price-and-inventory")]
  Task<TrendyolApiResult<ResponseBatchRequestId>> UpdatePriceAndInventoryAsync(
    [Body] RequestUpdatePriceAndInventory request
  );

  /// <summary>
  /// Deletes products from Trendyol.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-silme
  /// </summary>
  /// <param name="request">Request body containing product deletion details</param>
  /// <returns>Trendyol API response containing batch request ID</returns>
  [Delete("/integration/product/sellers/{SID}/products")]
  Task<TrendyolApiResult<ResponseBatchRequestId>> DeleteProductAsync(
    [Body] RequestDeleteProducts request
  );

  /// <summary>
  /// Retrieves the result of a batch request.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/toplu-islem-kontrolu
  /// </summary>
  /// <param name="batchRequestId">Batch request ID</param>
  /// <returns>Trendyol API response containing batch request details</returns>
  [Get("/integration/product/sellers/{SID}/products/batch-requests/{batchRequestId}")]
  Task<TrendyolApiResult<ResponseGetBatchRequestResult>> GetBatchRequestResultAsync(
    [AliasAs("batchRequestId")] string batchRequestId
  );

  /// <summary>
  /// Retrieves products based on filters.
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-filtreleme
  /// </summary>
  /// <param name="filter">Filter parameters for product retrieval</param>
  /// <returns>Trendyol API response containing filtered product list</returns>
  [Get("/integration/product/sellers/{SID}/products")]
  Task<TrendyolApiResult<ResponseGetProductsFiltered>> GetProductsAsync(
    [Query] FilterProducts filter
  );

  /// <summary>
  /// Retrieves customer questions based on filters.
  /// https://developers.trendyol.com/docs/marketplace/soru-cevap-entegrasyonu/musteri-sorularini-cekme
  /// </summary>
  /// <param name="filter">Filter parameters for retrieving customer questions</param>
  /// <returns>Trendyol API response containing filtered customer questions</returns>
  [Get("/integration/qna/sellers/{SID}/questions/filter")]
  Task<TrendyolApiResult<ResponseGetQuestions>> GetQuestionsAsync(
    [Query] FilterGetQuestions filter
  );

  /// <summary>
  /// Retrieves a customer question by its ID.
  /// https://developers.trendyol.com/docs/marketplace/soru-cevap-entegrasyonu/musteri-sorularini-cekme
  /// </summary>
  /// <param name="id">Question ID</param>
  /// <returns>Trendyol API response containing the question details</returns>
  [Get("/integration/qna/sellers/{SID}/questions/{id}")]
  Task<TrendyolApiResult<Question>> GetQuestionsByIdAsync(
    [AliasAs("id")] int id
  );

  /// <summary>
  /// Submits an answer to a customer question.
  /// https://developers.trendyol.com/docs/marketplace/soru-cevap-entegrasyonu/musteri-sorularini-cevaplama
  /// </summary>
  /// <param name="id">Question ID</param>
  /// <param name="request">Request body containing the answer details</param>
  /// <returns>Trendyol API response</returns>
  [Post("/integration/qna/sellers/{SID}/questions/{id}/answers")]
  Task<TrendyolApiResult> CreateAnswerAsync(
    [AliasAs("id")] long id,
    [Body] RequestCreateAnswer request
  );

  /// <summary>
  /// Retrieves created return orders based on filters.
  /// https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-olusturulan-siparisleri-cekme
  /// </summary>
  /// <param name="filter">Filter parameters for retrieving return orders</param>
  /// <returns>Trendyol API response containing return orders</returns>
  [Get("/integration/order/sellers/{SID}/claims")]
  Task<TrendyolApiResult<ResponseGetClaims>> GetClaimsAsync(
    [Query] RequestGetClaims filter
  );

  /// <summary>
  /// Approves claim line items for a given claim.
  /// https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-siparisleri-onaylama
  /// </summary>
  /// <param name="claimId">Claim ID</param>
  /// <param name="request">Request body containing approval details</param>
  /// <returns>Trendyol API response</returns>
  [Put("/integration/order/sellers/{SID}/claims/{claimId}/items/approve")]
  Task<TrendyolApiResult> ApproveClaimLineItemsAsync(
    [AliasAs("claimId")] string claimId,
    [Body] RequestApproveClaimLineItems request
  );

  /// <summary>
  /// Creates a claim issue (return rejection request).
  /// https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-siparislerinde-red-talebi-olusturma
  /// </summary>
  /// <param name="request">Request body containing claim issue details</param>
  /// <returns>Trendyol API response containing claim issue details</returns>
  [Post("/integration/order/sellers/{SID}/claims/{ClaimId}/issue")]
  Task<TrendyolApiResult<ResponseCreateClaim>> CreateClaimIssueAsync(
    [Body] RequestCreateClaimIssue request
  );

  /// <summary>
  /// Retrieves reasons for return rejections.
  /// https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-red-sebeplerini-cekme
  /// </summary>
  /// <returns>Trendyol API response containing claim issue reasons</returns>
  [Get("/integration/order/claim-issue-reasons")]
  Task<TrendyolApiResult<ResponseGetClaimsIssueReasons>> GetClaimsIssueReasonsAsync();

  /// <summary>
  /// Retrieves audit information for a specific claim item.
  /// https://developers.trendyol.com/docs/marketplace/iade-entegrasyonu/iade-audit-bilgilerini-cekme
  /// </summary>
  /// <param name="claimItemsId">Claim item ID</param>
  /// <returns>Trendyol API response containing claim audit information</returns>
  [Get("/integration/order/sellers/{SID}/claims/items/{claimItemsId}/audit")]
  Task<TrendyolApiResult<ResponseGetClaimAuditInformation>> GetClaimAuditInformationAsync(
    [AliasAs("claimItemsId")] string claimItemsId
  );
}