using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolGlobalOffer
{
    [JsonPropertyName("variantId")]
    public string VariantId { get; set; }

    [JsonPropertyName("modelNo")]
    public string ModelNo { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("brandId")]
    public long BrandId { get; set; }

    [JsonPropertyName("brandName")]
    public string BrandName { get; set; }

    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

    [JsonPropertyName("size")]
    public string Size { get; set; }

    [JsonPropertyName("attributes")]
    public List<TrendyolGlobalOfferAttribute> Attributes { get; set; }

    [JsonPropertyName("prices")]
    public TrendyolGlobalOfferPrices Prices { get; set; }

    [JsonPropertyName("images")]
    public List<TrendyolGlobalOfferImage> Images { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }

    /// <summary>
    ///     RFC3339 date-time
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public string LastModifiedDate { get; set; }
}
