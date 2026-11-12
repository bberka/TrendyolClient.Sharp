using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Line of an International test order. The product must be approved.</summary>
public class TrendyolIntlTestOrderLine
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>Simulates a seller discount; 0 is ignored.</summary>
    [JsonPropertyName("discountPercentage")]
    public decimal? DiscountPercentage { get; set; }

    /// <summary>Simulates a Trendyol discount. Only in the GULF guide sample, not in the reference.</summary>
    [JsonPropertyName("trendyolDiscountPercentage")]
    public decimal? TrendyolDiscountPercentage { get; set; }
}
