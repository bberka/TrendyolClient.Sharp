using System;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseAddIpWhitelist
{
    [JsonPropertyName("sellerId")]
    public string SellerId { get; set; }

    /// <summary>
    ///     Added IPv4 address
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    /// <summary>
    ///     Expiry of the entry (ISO-8601, UTC)
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset ExpiresAt { get; set; }
}
