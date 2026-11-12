using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolOrderLine
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("discountPercentage")]
    public decimal? DiscountPercentage { get; set; }

    /// <summary>
    ///     Percentage of the line price discounted by Trendyol (simulates Trendyol-funded discounts).
    /// </summary>
    [JsonPropertyName("trendyolDiscountPercentage")]
    public decimal? TrendyolDiscountPercentage { get; set; }
}