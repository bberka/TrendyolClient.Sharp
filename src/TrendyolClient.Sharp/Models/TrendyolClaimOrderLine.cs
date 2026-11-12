using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolClaimOrderLine
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("merchantSku")]
    public string MerchantSku { get; set; }

    [JsonPropertyName("productColor")]
    public string ProductColor { get; set; }

    [JsonPropertyName("productSize")]
    public string ProductSize { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>Deprecated alias of <see cref="VatRate" />. The docs examples still return it alongside vatRate.</summary>
    [JsonPropertyName("vatBaseAmount")]
    public decimal VatBaseAmount { get; set; }

    /// <summary>VAT rate (replaces vatBaseAmount).</summary>
    [JsonPropertyName("vatRate")]
    public decimal VatRate { get; set; }

    [JsonPropertyName("salesCampaignId")]
    public long SalesCampaignId { get; set; }

    [JsonPropertyName("productCategory")]
    public string ProductCategory { get; set; }
}
