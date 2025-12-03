using System;

namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when API rate limit is exceeded
/// </summary>
internal class TrendyolRateLimitException : TrendyolException
{
    public TrendyolRateLimitException(string message, DateTimeOffset? retryAfter = null) : base(message)
    {
        RetryAfter = retryAfter ?? DateTimeOffset.UtcNow.AddSeconds(60);
    }

    public TrendyolRateLimitException(DateTime retryAfter) : base($"Rate limit exceeded. Please retry after {retryAfter:yyyy-MM-dd HH:mm:ss} UTC")
    {
        RetryAfter = retryAfter;
    }

    /// <summary>
    ///     When the rate limit will reset and requests can be made again
    /// </summary>
    public DateTimeOffset RetryAfter { get; }

    /// <summary>
    ///     Number of seconds to wait before retrying
    /// </summary>
    public int RetryAfterSeconds => (int)(RetryAfter - DateTime.UtcNow).TotalSeconds;

    public override string ToString()
    {
        return $"{Message} (Retry after: {RetryAfter:yyyy-MM-dd HH:mm:ss} UTC, {RetryAfterSeconds}s from now)";
    }
}