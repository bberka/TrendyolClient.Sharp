namespace TrendyolClient.Sharp.Handlers;

/// <summary>
///     The names the factory, the handlers and the interface routes agree on. Routes use <see cref="SellerIdToken" /> in place of the
///     seller id because the id belongs to the client, not to each call; <c>TrendyolSellerIdInjectorMessageHandler</c> replaces it.
/// </summary>
internal static class TrendyolInternals
{
    /// <summary>Name of the <c>HttpClient</c> every Trendyol client is built on.</summary>
    public const string HttpClientName = "TrendyolClient";

    /// <summary>Placeholder in route templates and in the <c>SellerID</c> header that stands for the seller id.</summary>
    public const string SellerIdToken = "SELLER_ID_PARAMETER";

    /// <summary>
    ///     Carries the seller id from the factory to the handlers. It is removed by <c>TrendyolWireCleanupHandler</c> before the
    ///     request leaves, so it never reaches Trendyol.
    /// </summary>
    public const string SellerIdHeader = "X-Internal-Seller-Id";

    /// <summary>Header some endpoints (test orders) take the seller id in.</summary>
    public const string SellerIdRequestHeader = "SellerID";
}
