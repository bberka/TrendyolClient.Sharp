using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TrendyolClient.Sharp.Models;

namespace TrendyolClient.Sharp;

/// <summary>
///     Trendyol Global Partner / Global Center Gateway API (international e-export): product offers, stock and
///     purchase orders. Docs: developers.trendyol.com/v5.0.
///     Production host https://apigw.trendyol.com, stage https://stageapigw.trendyol.com, base path
///     /integration/exportcentergw. Authentication is HTTP Basic with the same API key/secret as the marketplace.
///     The routes carry no seller ID, so the SELLER_ID_PARAMETER token is not used. Every call requires the headers
///     x-correlationid (new UUIDv4 per request) and x-agentname (client/integrator name); unlike the Export Center
///     there is no x-clientip header. Paging is body based: the pageKey of a response goes into the next request.
///     No rate limits are documented. Errors use the common { status, type, traceId, error { code, title, detail } }
///     body (codes 1001-1007, 2001-2009).
/// </summary>
public interface ITrendyolGlobalPartnerApi
{
    /// <summary>
    ///     Lists product offers (max 100 per page). All filters are optional and can be combined. Pass the returned
    ///     <c>pageKey</c> as <c>PageKey</c> of the next call.
    /// </summary>
    [Get("/integration/exportcentergw/v1/offers")]
    Task<IApiResponse<TrendyolGlobalResponseGetOffers>> GetOffersAsync(
        [Query] TrendyolGlobalFilterGetOffers filter,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets media content (images, size charts) of offers by content ID.
    /// </summary>
    [Get("/integration/exportcentergw/v1/offers/content-medias")]
    Task<IApiResponse<TrendyolGlobalResponseGetOfferContentMedias>> GetOfferContentMediasAsync(
        [Query] TrendyolGlobalFilterGetOfferContentMedias filter,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets stock for up to 20 variant IDs, split into Trendyol and seller warehouses.
    /// </summary>
    [Get("/integration/exportcentergw/v1/stocks")]
    Task<IApiResponse<List<TrendyolGlobalStock>>> GetStocksAsync(
        [Query] TrendyolGlobalFilterGetStocks filter,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Creates an order (max 100 items, requestedQuantity greater than 0). The vendorRef must be a unique UUIDv4
    ///     for each order.
    /// </summary>
    [Post("/integration/exportcentergw/v1/orders")]
    Task<IApiResponse<TrendyolGlobalResponseCreateOrder>> CreateOrderAsync(
        [Body] TrendyolGlobalRequestCreateOrder request,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Cancels an order or specific products of it (max 100 items). Only cancelable products can be cancelled
    ///     (error 2004/2009 otherwise). Returns an empty JSON object.
    /// </summary>
    /// <param name="orderNumber">Order number returned by <see cref="CreateOrderAsync" /></param>
    [Post("/integration/exportcentergw/v1/orders/{orderNumber}/cancel")]
    Task<IApiResponse> CancelOrderAsync(
        [AliasAs("orderNumber")] string orderNumber,
        [Body] TrendyolGlobalRequestCancelOrder request,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the order references (vendorRef) associated with a B2B ID.
    /// </summary>
    [Get("/integration/exportcentergw/v1/orders/vendorRefs/{b2bId}")]
    Task<IApiResponse<TrendyolGlobalResponseGetVendorRefsByB2BId>> GetVendorRefsByB2BIdAsync(
        [AliasAs("b2bId")] long b2bId,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );

    /// <summary>
    ///     Gets the B2B records associated with a vendorRef. Error 2005 means no B2B has been created for the
    ///     vendorRef yet.
    /// </summary>
    [Get("/integration/exportcentergw/v1/orders/{vendorRef}/b2bs")]
    Task<IApiResponse<TrendyolGlobalResponseGetB2BsByVendorRef>> GetB2BsByVendorRefAsync(
        [AliasAs("vendorRef")] string vendorRef,
        [Header("x-correlationid")] string correlationId,
        [Header("x-agentname")] string agentName
    );
}
