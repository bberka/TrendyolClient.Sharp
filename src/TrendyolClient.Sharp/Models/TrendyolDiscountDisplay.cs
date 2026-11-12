using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Campaign name and amount shown for a package discount.
/// </summary>
public class TrendyolDiscountDisplay
{
    /// <summary>
    ///     Campaign name.
    /// </summary>
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("discountAmount")]
    public decimal DiscountAmount { get; set; }
}
