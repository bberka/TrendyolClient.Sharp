using TrendyolClient.Sharp.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     A webhook subscription.
/// </summary>
public class TrendyolWebhook
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     Milliseconds, GMT+3.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public long CreatedDate { get; set; }

    /// <summary>
    ///     Milliseconds, GMT+3.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public long? LastModifiedDate { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("authenticationType")]
    public TrendyolWebhookAuthenticationType AuthenticationType { get; set; }

    /// <summary>
    ///     "ACTIVE" or "PASSIVE". Trendyol passivates a webhook after repeated failures.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("subscribedStatuses")]
    public List<TrendyolOrderStatus> SubscribedStatuses { get; set; }
}
