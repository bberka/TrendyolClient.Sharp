using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolBuyboxInfo
{
    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    [JsonPropertyName("buyboxOrder")]
    public int BuyboxOrder { get; set; }

    [JsonPropertyName("buyboxPrice")]
    public decimal BuyboxPrice { get; set; }

    [JsonPropertyName("hasMultipleSeller")]
    public bool HasMultipleSeller { get; set; }

    [JsonPropertyName("secondBuyboxPrice")]
    public decimal? SecondBuyboxPrice { get; set; }

    [JsonPropertyName("thirdBuyboxPrice")]
    public decimal? ThirdBuyboxPrice { get; set; }
}
