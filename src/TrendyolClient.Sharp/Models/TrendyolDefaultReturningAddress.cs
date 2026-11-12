using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Default return address marker as documented by the addresses service
/// </summary>
public class TrendyolDefaultReturningAddress
{
    /// <summary>
    ///     Whether a default return address exists
    /// </summary>
    [JsonPropertyName("present")]
    public bool Present { get; set; }
}
