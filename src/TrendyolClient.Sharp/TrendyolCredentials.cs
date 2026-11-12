namespace TrendyolClient.Sharp;

/// <summary>
///     Everything that identifies one seller account: pass it to the factory instead of repeating four arguments, or register it
///     once with <c>AddTrendyolSeller</c>.
/// </summary>
/// <param name="SellerId">Seller (supplier) id.</param>
/// <param name="ApiKey">API key from the seller panel.</param>
/// <param name="ApiSecret">API secret from the seller panel.</param>
/// <param name="UseStageApi">Use <c>stageapigw.trendyol.com</c>. Stage has its own seller id, key and secret.</param>
/// <param name="StoreFrontCode">
///     International storefront (DE, SA, AE, KW, GR, SK, RO, CZ, BG), sent as the <c>storeFrontCode</c> header on every request.
///     Leave <c>null</c> for the Türkiye marketplace. Required for the International client.
/// </param>
public sealed record TrendyolCredentials(
    long SellerId,
    string ApiKey,
    string ApiSecret,
    bool UseStageApi = false,
    string StoreFrontCode = null
)
{
    /// <summary>
    ///     The key and secret are left out, so a credentials object that ends up in a log line does not leak them.
    /// </summary>
    public override string ToString()
    {
        return $"TrendyolCredentials {{ SellerId = {SellerId}, UseStageApi = {UseStageApi}, StoreFrontCode = {StoreFrontCode} }}";
    }
}
