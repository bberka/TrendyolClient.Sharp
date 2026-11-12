using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportCreateProductItem
{
    /// <summary>
    ///     Marketplace barcode of an approved product (required)
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Price Trendyol buys the product at, VAT included (required)
    /// </summary>
    [JsonPropertyName("buyingPrice")]
    public decimal BuyingPrice { get; set; }

    /// <summary>
    ///     Recommended retail price; must be greater than or equal to BuyingPrice
    /// </summary>
    [JsonPropertyName("rrp")]
    public decimal? Rrp { get; set; }

    /// <summary>
    ///     GTIP number, 4 to 12 digits
    /// </summary>
    [JsonPropertyName("gtip")]
    public string Gtip { get; set; }

    /// <summary>
    ///     Country of origin, a name from the origins lookup (required)
    /// </summary>
    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    /// <summary>
    ///     Current stock (required)
    /// </summary>
    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    /// <summary>
    ///     Marketplace category ID (required)
    /// </summary>
    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    /// <summary>
    ///     Required category attributes (required)
    /// </summary>
    [JsonPropertyName("attributes")]
    public List<TrendyolExportProductAttributeInput> Attributes { get; set; }
}
