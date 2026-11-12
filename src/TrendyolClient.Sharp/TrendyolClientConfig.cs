namespace TrendyolClient.Sharp;

/// <summary>
///     Configuration options for TrendyolClient
/// </summary>
public class TrendyolClientConfig
{
    /// <summary>
    ///     Name of your integration (appears in the User-Agent header as "{sellerId} - {IntegrationName}").
    ///     Trendyol requires alphanumeric characters only, at most 30 characters; requests without a User-Agent are rejected with 403.
    ///     Use "SelfIntegration" when the software belongs to the seller itself.
    ///     Default: "SelfIntegration"
    /// </summary>
    public string IntegrationName { get; set; } = "SelfIntegration";

    /// <summary>
    ///     Request timeout in seconds
    ///     Default: 30 seconds
    /// </summary>
    public int RequestTimeoutSeconds { get; set; } = 30;

    /// <summary>
    ///     Enable request/response logging
    ///     Default: true
    /// </summary>
    public bool EnableLogging { get; set; } = true;

    /// <summary>
    ///     Throw the typed exceptions in <c>TrendyolClient.Sharp.Exceptions</c> (for example
    ///     <c>TrendyolRateLimitException</c>) for non-success HTTP responses.
    ///     When <c>false</c>, no exception is thrown: check <c>IsSuccessStatusCode</c> and read <c>Error</c> on the response instead.
    ///     Default: false
    /// </summary>
    public bool ThrowOnError { get; set; }
}