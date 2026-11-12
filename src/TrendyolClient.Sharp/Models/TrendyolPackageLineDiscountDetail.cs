using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Discount applied to a single unit. A line with quantity N carries N of these.
/// </summary>
public class TrendyolPackageLineDiscountDetail
{
    [JsonPropertyName("lineItemId")]
    public string LineItemId { get; set; }

    /// <summary>
    ///     Discounted unit price: lineGrossAmount - lineItemSellerDiscount - lineItemTyDiscount.
    /// </summary>
    [JsonPropertyName("lineItemPrice")]
    public decimal LineItemPrice { get; set; }

    [JsonPropertyName("lineItemSellerDiscount")]
    public decimal LineItemSellerDiscount { get; set; }

    [JsonPropertyName("lineItemTyDiscount")]
    public decimal LineItemTyDiscount { get; set; }
}
