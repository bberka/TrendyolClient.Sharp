﻿using System.Runtime;
using System.Text;
using Serilog;
using TrendyolSharp.Marketplace.Models;
using TrendyolSharp.Marketplace.Models.Filter;
using TrendyolSharp.Marketplace.Models.Request;
using TrendyolSharp.Marketplace.Models.Response;
using TrendyolSharp.Shared;
using TrendyolSharp.Shared.Common;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models;


namespace TrendyolSharp.Marketplace;

public partial class TrendyolMarketplaceClient
{


  #region PRODUCT INTEGRATION

  //LINK: https://developers.trendyol.com/docs/category/%C3%BCr%C3%BCn-entegrasyonu
  //EXAMPLE REQUESTS: https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/ornek-istekler

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/iade-ve-sevkiyat-adres-bilgileri
  /// <br/><br/>
  /// In requests to createProduct V2 service, the order and shipping company information to be sent and the ID values ​​of this information will be obtained using this service.
  ///<br/><br/>
  ///If "SELLER APPLICATION PROCESS" is not fully completed, you should not use this service.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetSuppliersAddresses>> GetSupplierAddressesAsync() {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/addresses";
    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetSuppliersAddresses>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  ///  <br/><br/>
  ///  The brandId information to be sent to the createProduct V2 service will be obtained using this service.
  ///  <br/><br/>
  /// Minumum 1000 brands information can be provided on a page.
  ///   When searching for a brand, you need to create a query using the page parameter to the service.
  /// </summary>
  /// <param name="pageRequest"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetBrands>> GetBrandsAsync(FilterPage? pageRequest = null) {
    var url = "https://api.trendyol.com/sapigw/brands";
    if (pageRequest is not null) {
      //for each prop if not null add to url
      url += $"&page={pageRequest.Page}";
      url += $"&size={pageRequest.Size}";
    }

    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetBrands>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-marka-listesi
  ///  <br/><br/>
  /// Brand names are case sensitive.
  /// </summary>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetBrandsByName>> GetBrandsByNameAsync(string name) {
    var url = $"https://api.trendyol.com/sapigw/brands/by-name&name={name}";
    if (string.IsNullOrEmpty(name)) {
      throw new ArgumentNullException(nameof(name));
    }

    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetBrandsByName>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-listesi
  ///  <br/><br/>
  ///  The categoryId information to be sent in requests to the createProduct V2 service will be obtained using this service.
  ///  <br/><br/>
  ///  Category ID information should be used at the lowest level ("subCategories": []) to create createProduct. If there are subcategories of the category you select, you cannot transfer products with this category.
  /// <br/><br/>
  /// We recommend that you get the updated category list weekly, as new categories may be added
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetCategoryTree>> GetCategoryTreeAsync(FilterGetCategoryTree? filter = null) {
    var url = "https://api.trendyol.com/sapigw/product-categories";
    if (filter is not null) {
      //for each prop if not null add to url
      if (filter.Id.HasValue) {
        url += $"&id={filter.Id}";
      }

      if (filter.ParentId.HasValue) {
        url += $"&parentId={filter.ParentId}";
      }

      if (!string.IsNullOrEmpty(filter.Name)) {
        url += $"&name={filter.Name}";
      }

      if (filter.SubCategories.HasValue) {
        url += $"&subCategories={filter.SubCategories}";
      }
    }

    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetCategoryTree>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-kategori-ozellik-listesi
  ///  <br/><br/>
  /// The attributes information to be sent in requests to the createProduct V2 service and the details of this information will be obtained using this service.
  ///  <br/><br/>
  /// Category ID information should be used at the lowest level ("subCategories": []) to create createProduct. If there are subcategories of the category you select, you cannot transfer products with this category.
  /// <br/><br/>
  /// We recommend that you get the updated category attribute list on a weekly basis, as new category attributes can be added.
  /// </summary>
  /// <param name="categoryId"></param>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetCategoryAttributes>> GetCategoryAttributesAsync(int categoryId, FilterGetCategoryAttributes? filter = null) {
    var url = $"https://api.trendyol.com/sapigw/product-categories/{categoryId}/attributes";
    if (filter is not null) {
      //for each prop if not null add to url
      if (!string.IsNullOrEmpty(filter.Name)) {
        url += $"&name={filter.Name}";
      }

      if (!string.IsNullOrEmpty(filter.DisplayName)) {
        url += $"&displayName={filter.DisplayName}";
      }

      if (filter.AttributeId.HasValue) {
        url += $"&attributeId={filter.AttributeId}";
      }

      if (!string.IsNullOrEmpty(filter.AttributeName)) {
        url += $"&attributeName={filter.AttributeName}";
      }

      if (filter.AllowCustom.HasValue) {
        url += $"&allowCustom={filter.AllowCustom}";
      }

      if (filter.Required.HasValue) {
        url += $"&required={filter.Required}";
      }

      if (filter.Slicer.HasValue) {
        url += $"&slicer={filter.Slicer}";
      }

      if (filter.Varianter.HasValue) {
        url += $"&varianter={filter.Varianter}";
      }

      if (filter.AttributeValueId.HasValue) {
        url += $"&attributeValueId={filter.AttributeValueId}";
      }

      if (!string.IsNullOrEmpty(filter.AttributeValueName)) {
        url += $"&attributeValueName={filter.AttributeValueName}";
      }
    }

    var request = new TrendyolRequest(_httpClient, url.Replace("{categoriId}", categoryId.ToString()));
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetCategoryAttributes>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-aktarma-v2
  ///  <br/><br/>
  /// This method is used when uploading your products to the Trendyol system. It supports single and multiple product shipment.
  ///  <br/><br/>
  /// You must determine the prices of your products in Turkish Lira. Exchange rate information is not supported.
  /// <br/>
  /// Before transferring the product with this method, relevant details should be obtained from Trendyol Brand List and Category/Category Attribute Information services.
  /// <br/>
  ///
  /// The maximum number of items that can be sent in each request is 1000.
  /// <br/>
  /// In order to be able to define in the fastDeliveryType field, the deliveryDuration field must be entered as 1.
  /// <br/>
  /// "stockCode" in product data, equals to "merchantSku" in order data. It can be checked in getShipmentPackages service.
  ///  <br/> <br/>
  /// After the product create process, you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// <br/> <br/>
  ///Service Responses
  /// <br/>
  ///200	The request was successful. you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// <br/>
  ///400	Missing or incorrect parameter is used in the URL. Review the document again.
  /// <br/>
  ///401	One of the supplierID, API Key, API Secure Key information you used while sending the request is missing or incorrect. You can find the right information for your store on Trendyol Seller Panel.
  /// <br/>
  ///404	The request url information is incorrect. Review the document again.
  /// <br/>
  ///500	There may have been a momentary error. In case the situation does not improve by waiting a few minutes, create a request under the heading "API Integration Support Request" with the endpoint used, the sent request and the
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> CreateProductsAsync(RequestCreateProducts request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }


  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/trendyol-urun-bilgisi-guncelleme
  ///  <br/><br/>
  /// This method allows you to update the products that you have previously created using the createProduct V2 service in your Trendyol store.
  ///  <br/><br/>
  /// This service is specifically designed for updating product information only. If you need to update stock and price values, you should use the updatePriceAndInventory service.
  /// <br/><br/>
  /// As new category and category attribute values may be added, it is recommended to check the getCategoryTree and getCategoryAttributes services to ensure that the category and category attribute values you use are up-to-date before updating your products.
  ///  <br/><br/>
  /// The status of the product will be set to "İçerik Kontrol Bekleniyor" (Content Check Pending) after the update request. Your products may still be open for sale. If you don't want to receive orders during this period, it's crucial to update your stock and price information accordingly.
  ///  <br/><br/>
  /// You can include a maximum of 1000 items in each update request.
  ///  <br/><br/>
  /// Note that the productMainId value cannot be updated for approved products.
  ///  <br/><br/>
  /// After the product update process, you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> UpdateProductsAsync(RequestUpdateProduct request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/v2/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPutRequestAsync(request);
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/stok-ve-fiyat-guncelleme
  ///  <br/><br/>
  /// The price and stock information of the products created and approved to Trendyol can be updated at the same time.
  ///  <br/><br/>
  /// If you send the same request again without making any changes in the request body in the stock-price update process, an error message will be returned to you. You will see the error message "15 dakika boyunca aynı isteği tekrarlı olarak atamazsınız!". You just need to fix your systems so that you can send only your changing stock-prices.
  ///  <br/><br/>
  ///   The stock you submit in the Quantity field is the salable stock information. Sellable stock information is updated when an order is received or restocked by you.
  ///  <br/><br/>
  ///   You can update a maximum of 1000 items (sku) in stock-price update transactions.
  ///  <br/><br/>
  /// After the product stock and price update process, you need to check the status of your products and the transfer process via the getBatchRequestResult service with the batchRequestId in the response.
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> UpdatePriceAndInventoryAsync(RequestUpdatePriceAndInventory request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products/price-and-inventory";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendPostRequestAsync(request);
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-silme
  ///  <br/><br/>
  /// This method is used when removing your products from the Trendyol system. It supports single and multiple product deletion. You can delete your products pending approval and your approved products found in the archive for more than one day and not stopped for sale by Trendyol.
  ///  <br/><br/>
  /// After deleting the product, you need to check the status of your transaction with the batchRequestId in the response via the getBatchRequestResult service.
  /// </summary>
  /// <param name="request"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseBatchRequestId>> DeleteProductAsync(RequestDeleteProducts request) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendDeleteRequestAsync();
    var data = result.Content.ToObject<ResponseBatchRequestId>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/toplu-islem-kontrolu
  ///  <br/><br/>
  /// While createProducts, updatePriceAndInventory methods are processed by the queue in TrendyolSystem, a batchRequestId information is returned in each successful request result. By checking the "status" field in the return of the service, you can check whether the batch has been completed. If more than one item in the batch results in an error, the failureReasons field can be checked to see error reason.
  /// <br/><br/>
  /// You can see the batch request results' createProducts,updateProducts and updatePriceAndInventory services up to 4 hours later.
  ///  <br/><br/>
  ///  After the Stock Price update processes, you need to check the item-based status fields for the batchId you are querying. Batch status field will not return to you.
  /// </summary>
  /// <param name="batchRequestId"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetBatchRequestResult>> GetBatchRequestResultAsync(string batchRequestId) {
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products/batch-requests/{batchRequestId}";
    var trendyolRequest = new TrendyolRequest(_httpClient, url);
    var result = await trendyolRequest.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetBatchRequestResult>();
    return result.WithData(data);
  }

  /// <summary>
  /// https://developers.trendyol.com/docs/marketplace/urun-entegrasyonu/urun-filtreleme
  ///  <br/><br/>
  /// With this service, you can list your products in your Trendyol store.
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public async Task<TrendyolApiResult<ResponseGetProductsFiltered>> GetProductsAsync(FilterProducts? filter = null) {
    //TODO: Check if can take all products without filtering
    var url = $"https://api.trendyol.com/sapigw/suppliers/{_supplierId}/products";
    if (filter is not null) {
      if (filter.Approved.HasValue) {
        url += $"&approved={filter.Approved}";
      }

      if (!string.IsNullOrEmpty(filter.Barcode)) {
        url += $"&barcode={filter.Barcode}";
      }

      if (filter.StartDate.HasValue) {
        url += $"&startDate={filter.StartDate}";
      }

      if (filter.EndDate.HasValue) {
        url += $"&endDate={filter.EndDate}";
      }

      if (filter.Page.HasValue) {
        url += $"&page={filter.Page}";
      }

      if (!string.IsNullOrEmpty(filter.DateQueryType)) {
        url += $"&dateQueryType={filter.DateQueryType}";
      }

      if (filter.Size.HasValue) {
        url += $"&size={filter.Size}";
      }

      if (filter.SupplierId.HasValue) {
        url += $"&supplierId={filter.SupplierId}";
      }

      if (!string.IsNullOrEmpty(filter.StockCode)) {
        url += $"&stockCode={filter.StockCode}";
      }

      if (filter.Archived.HasValue) {
        url += $"&archived={filter.Archived}";
      }

      if (!string.IsNullOrEmpty(filter.ProductMainId)) {
        url += $"&productMainId={filter.ProductMainId}";
      }

      if (filter.OnSale.HasValue) {
        url += $"&onSale={filter.OnSale}";
      }

      if (filter.Rejected.HasValue) {
        url += $"&rejected={filter.Rejected}";
      }

      if (filter.Blacklisted.HasValue) {
        url += $"&blacklisted={filter.Blacklisted}";
      }

      if (filter.BrandIds is not null) {
        foreach (var brandId in filter.BrandIds) {
          url += $"&brandIds={brandId}";
        }
      }
    }

    var request = new TrendyolRequest(_httpClient, url);
    var result = await request.SendGetRequestAsync();
    var data = result.Content.ToObject<ResponseGetProductsFiltered>();
    return result.WithData(data);
  }

  #endregion

}