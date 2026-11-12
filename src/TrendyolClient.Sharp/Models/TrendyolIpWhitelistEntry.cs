using System;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     IP whitelist entry for stage access
/// </summary>
public class TrendyolIpWhitelistEntry
{
    /// <summary>
    ///     Whitelisted IPv4 address
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    /// <summary>
    ///     Expiry of the entry (ISO-8601, UTC)
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset ExpiresAt { get; set; }
}
