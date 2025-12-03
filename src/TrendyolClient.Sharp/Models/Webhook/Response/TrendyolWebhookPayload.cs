using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models.Webhook.Response;

/// <summary>
///     Webhook payload structure that Trendyol sends to your endpoint
///     This is what you'll receive on your webhook URL
/// </summary>
public class TrendyolWebhookPayload
{
    /// <summary>
    ///     Unique event identifier
    /// </summary>
    [JsonPropertyName("eventId")]
    public string EventId { get; set; }

    /// <summary>
    ///     Timestamp when the event occurred
    /// </summary>
    [JsonPropertyName("eventTime")]
    public DateTime EventTime { get; set; }

    /// <summary>
    ///     The order status that triggered this webhook
    /// </summary>
    [JsonPropertyName("status")]
    public TrendyolOrderStatus Status { get; set; }

    /// <summary>
    ///     Shipment package ID
    /// </summary>
    [JsonPropertyName("shipmentPackageId")]
    public long ShipmentPackageId { get; set; }

    /// <summary>
    ///     Order number
    /// </summary>
    [JsonPropertyName("orderNumber")]
    public string OrderNumber { get; set; }

    /// <summary>
    ///     Seller ID
    /// </summary>
    [JsonPropertyName("sellerId")]
    public long SellerId { get; set; }

    /// <summary>
    ///     Additional event data specific to the status change
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, object> Data { get; set; }
}