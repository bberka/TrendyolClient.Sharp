using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportProduct
{
    /// <summary>
    ///     Trendyol barcode
    /// </summary>
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    /// <summary>
    ///     Barcode in the seller systems
    /// </summary>
    [JsonPropertyName("sellerBarcode")]
    public string SellerBarcode { get; set; }

    [JsonPropertyName("rrpPrice")]
    public decimal RrpPrice { get; set; }

    [JsonPropertyName("buyingPrice")]
    public decimal BuyingPrice { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    /// <summary>
    ///     Origin country code
    /// </summary>
    [JsonPropertyName("origin")]
    public string Origin { get; set; }

    [JsonPropertyName("composition")]
    public string Composition { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Currency code (EUR, USD...), chosen at Export Center application
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("gtip")]
    public string Gtip { get; set; }

    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    /// <summary>
    ///     Care instruction code
    /// </summary>
    [JsonPropertyName("careInstructions")]
    public string CareInstructions { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolExportProductAttribute> Attributes { get; set; }
}
