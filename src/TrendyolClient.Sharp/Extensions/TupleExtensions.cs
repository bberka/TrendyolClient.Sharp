namespace TrendyolClient.Sharp.Extensions;

// Extension method for cleaner tuple unpacking
internal static class TupleExtensions
{
    public static (ITrendyolMarketplaceApi marketplace, ITrendyolWebhookApi webhook) AsValueTuple(
        this (ITrendyolMarketplaceApi marketplace, ITrendyolWebhookApi webhook, string apiKey, string apiSecret) tuple
    )
    {
        return (tuple.marketplace, tuple.webhook);
    }
}