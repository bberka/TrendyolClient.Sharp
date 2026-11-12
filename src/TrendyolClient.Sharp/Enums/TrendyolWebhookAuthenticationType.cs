using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     Authentication types supported by Trendyol webhooks ("BASIC_AUTHENTICATION" or "API_KEY").
/// </summary>
[JsonConverter(typeof(TrendyolUpperSnakeCaseEnumConverter))]
public enum TrendyolWebhookAuthenticationType
{
    /// <summary>
    ///     Requires <c>username</c> and <c>password</c>.
    /// </summary>
    BasicAuthentication,

    /// <summary>
    ///     Requires <c>apiKey</c>; Trendyol sends it in the <c>x-api-key</c> header.
    /// </summary>
    ApiKey
}
