using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Response model for a single webhook subscription
/// </summary>
public class TrendyolResponseWebhook
{
    /// <summary>
    ///     Unique identifier for the webhook
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    ///     The URL that receives webhook notifications
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    ///     Authentication type used for the webhook endpoint
    /// </summary>
    [JsonPropertyName("authenticationType")]
    public TrendyolWebhookAuthenticationType AuthenticationType { get; set; }

    /// <summary>
    ///     List of order statuses that trigger webhook notifications
    /// </summary>
    [JsonPropertyName("subscribedStatuses")]
    public List<TrendyolOrderStatus> SubscribedStatuses { get; set; }

    /// <summary>
    ///     Whether the webhook is currently active
    /// </summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    /// <summary>
    ///     When the webhook was created
    /// </summary>
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    ///     When the webhook was last modified
    /// </summary>
    [JsonPropertyName("modifiedDate")]
    public DateTime? ModifiedDate { get; set; }
}