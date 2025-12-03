using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models.Webhook.Request;

/// <summary>
///     Request model for updating an existing webhook subscription
/// </summary>
public class TrendyolRequestUpdateWebhook
{
    /// <summary>
    ///     The URL that will receive webhook notifications (HTTPS recommended)
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    ///     Username for Basic authentication (required if AuthenticationType is BASIC)
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; }

    /// <summary>
    ///     Password for Basic authentication (required if AuthenticationType is BASIC)
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; }

    /// <summary>
    ///     Authentication method for webhook endpoint
    /// </summary>
    [JsonPropertyName("authenticationType")]
    public TrendyolWebhookAuthenticationType AuthenticationType { get; set; }

    /// <summary>
    ///     API Key for API_KEY authentication (required if AuthenticationType is API_KEY)
    /// </summary>
    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; }

    /// <summary>
    ///     List of order statuses that will trigger webhook notifications
    /// </summary>
    [JsonPropertyName("subscribedStatuses")]
    public List<TrendyolOrderStatus> SubscribedStatuses { get; set; }
}