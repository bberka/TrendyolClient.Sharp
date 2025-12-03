using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models.Webhook.Response;

/// <summary>
///     Response model for listing all webhook subscriptions
/// </summary>
public class TrendyolResponseGetWebhooks
{
    /// <summary>
    ///     List of webhook subscriptions
    /// </summary>
    [JsonPropertyName("webhooks")]
    public List<TrendyolResponseWebhook> Webhooks { get; set; }

    /// <summary>
    ///     Total number of webhooks
    /// </summary>
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }
}