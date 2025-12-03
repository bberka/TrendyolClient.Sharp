using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     Authentication types supported by Trendyol webhooks
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TrendyolWebhookAuthenticationType
{
    /// <summary>
    ///     Basic HTTP authentication with username and password
    /// </summary>
    [EnumMember(Value = "BASIC")]
    Basic,

    /// <summary>
    ///     API Key authentication
    /// </summary>
    [EnumMember(Value = "API_KEY")]
    ApiKey,

    /// <summary>
    ///     No authentication required
    /// </summary>
    [EnumMember(Value = "NONE")]
    None
}