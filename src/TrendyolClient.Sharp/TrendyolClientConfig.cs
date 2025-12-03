namespace TrendyolClient.Sharp;

/// <summary>
///     Configuration options for TrendyolClient
/// </summary>
public class TrendyolClientConfig
{
    /// <summary>
    ///     Name of your integration (appears in User-Agent header)
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
}