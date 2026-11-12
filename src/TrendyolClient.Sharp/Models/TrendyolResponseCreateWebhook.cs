using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseCreateWebhook
{
    /// <summary>
    ///     Webhook id used by update, delete, activate and deactivate.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
