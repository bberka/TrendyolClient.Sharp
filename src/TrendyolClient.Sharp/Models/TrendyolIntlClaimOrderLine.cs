using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolIntlClaimOrderLine
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

    /// <summary>SGR (deposit return scheme) fee. Only in the documented examples.</summary>
    [JsonPropertyName("totalSgrFee")]
    public decimal? TotalSgrFee { get; set; }

    [JsonPropertyName("vatBaseAmount")]
    public decimal VatBaseAmount { get; set; }

    [JsonPropertyName("vatRate")]
    public decimal VatRate { get; set; }

    [JsonPropertyName("salesCampaignId")]
    public long SalesCampaignId { get; set; }

    [JsonPropertyName("productCategory")]
    public string ProductCategory { get; set; }
}
