using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of createWebhook (International): the Marketplace body plus optional country filtering.</summary>
public class TrendyolIntlRequestCreateWebhook
{
    /// <summary>Must not contain "Trendyol", "Dolap" or "localhost".</summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>Optional storefront codes (for example SA, AE) to receive only those countries' orders; omitted means all active countries.</summary>
    [JsonPropertyName("countryCodes")]
    public List<string> CountryCodes { get; set; }

    /// <summary>Basic authentication user name.</summary>
    [JsonPropertyName("username")]
    public string Username { get; set; }

    /// <summary>Basic authentication password.</summary>
    [JsonPropertyName("password")]
    public string Password { get; set; }

    /// <summary>BASIC_AUTHENTICATION or API_KEY.</summary>
    [JsonPropertyName("authenticationType")]
    public TrendyolWebhookAuthenticationType AuthenticationType { get; set; }

    /// <summary>Sent as the x-api-key header when the type is API_KEY.</summary>
    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; }

    /// <summary>Empty subscribes to every status.</summary>
    [JsonPropertyName("subscribedStatuses")]
    public List<TrendyolOrderStatus> SubscribedStatuses { get; set; }
}
