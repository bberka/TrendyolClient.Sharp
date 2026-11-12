using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportPriceItem
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Recommended retail price; must be greater than or equal to BuyingPrice
    /// </summary>
    [JsonPropertyName("rrp")]
    public decimal? Rrp { get; set; }

    /// <summary>
    ///     Price Trendyol buys at, VAT included
    /// </summary>
    [JsonPropertyName("buyingPrice")]
    public decimal BuyingPrice { get; set; }
}
