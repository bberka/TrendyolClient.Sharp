using TrendyolClient.Sharp.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Request to update a webhook.
/// </summary>
public class TrendyolRequestUpdateWebhook
{
    /// <summary>
    ///     Webhook endpoint. Must not contain "Trendyol", "Dolap" or "Localhost".
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    ///     Required for BASIC_AUTHENTICATION.
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; }

    /// <summary>
    ///     Required for BASIC_AUTHENTICATION.
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("authenticationType")]
    public TrendyolWebhookAuthenticationType AuthenticationType { get; set; }

    /// <summary>
    ///     Required for API_KEY.
    /// </summary>
    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; }

    /// <summary>
    ///     When empty, all statuses (including future ones) are subscribed.
    /// </summary>
    [JsonPropertyName("subscribedStatuses")]
    public List<TrendyolOrderStatus> SubscribedStatuses { get; set; }
}
