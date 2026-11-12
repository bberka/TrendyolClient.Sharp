namespace TrendyolClient.Sharp;

/// <summary>
///     Client for the International Marketplace (Germany, CEE and GULF regions). Its product and order services are still on the V1
///     contracts, which the Türkiye <see cref="ITrendyolMarketplaceApi" /> no longer has, so it is a separate client.
///     Endpoints identical to the Türkiye ones (price and inventory, batch results, webhooks, ...) stay on the Marketplace client.
/// </summary>
public interface ITrendyolInternationalApi : ITrendyolInternationalOrderApi, ITrendyolInternationalClaimApi
{
}
