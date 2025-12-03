namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when authentication fails
/// </summary>
internal class TrendyolAuthenticationException(string message, long? sellerId = null) : TrendyolException(message)
{
    public TrendyolAuthenticationException(long sellerId) : this($"Authentication failed for seller {sellerId}. Please verify your API credentials.",
        sellerId)
    {
    }

    /// <summary>
    ///     The seller ID that failed authentication
    /// </summary>
    public long? SellerId { get; } = sellerId;

    public override string ToString()
    {
        var sellerInfo = SellerId.HasValue ? $" [SellerId: {SellerId}]" : "";
        return $"{Message}{sellerInfo}";
    }
}