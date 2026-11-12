using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolVariantPrice
{
    [JsonPropertyName("salePrice")]
    public decimal SalePrice { get; set; }

    [JsonPropertyName("listPrice")]
    public decimal ListPrice { get; set; }

    [JsonPropertyName("priceSeenByCustomer")]
    public decimal? PriceSeenByCustomer { get; set; }
}
