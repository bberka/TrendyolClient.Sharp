using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models;

/// <summary>A webhook registered by an International seller (getWebhooks).</summary>
public class TrendyolIntlWebhook
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>Timestamp, GMT+3.</summary>
    [JsonPropertyName("createdDate")]
    public long CreatedDate { get; set; }

    /// <summary>Timestamp, GMT+3.</summary>
    [JsonPropertyName("lastModifiedDate")]
    public long? LastModifiedDate { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>Storefront codes the webhook is limited to; empty or null means all.</summary>
    [JsonPropertyName("countryCodes")]
    public List<string> CountryCodes { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("authenticationType")]
    public TrendyolWebhookAuthenticationType AuthenticationType { get; set; }

    /// <summary>ACTIVE or PASSIVE.</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("subscribedStatuses")]
    public List<TrendyolOrderStatus> SubscribedStatuses { get; set; }

    /// <summary>Present in the guide sample; may be empty.</summary>
    [JsonPropertyName("storeFrontCode")]
    public string StoreFrontCode { get; set; }
}
