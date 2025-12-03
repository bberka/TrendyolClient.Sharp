using System;

namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when network/timeout errors occur
/// </summary>
internal class TrendyolNetworkException(string message, Exception innerException = null) : TrendyolException(message, innerException)
{
    public static TrendyolNetworkException Timeout(int timeoutSeconds)
    {
        return new TrendyolNetworkException($"Request timed out after {timeoutSeconds} seconds");
    }

    public static TrendyolNetworkException ConnectionFailed(string endpoint, Exception innerException = null)
    {
        return new TrendyolNetworkException($"Failed to connect to {endpoint}", innerException);
    }
}