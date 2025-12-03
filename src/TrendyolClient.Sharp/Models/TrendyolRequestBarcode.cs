using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestBarcode
{
    [JsonPropertyName("format")]
    public string Format { get; set; }

    [JsonPropertyName("boxQuantity")]
    public int? BoxQuantity { get; set; }

    [JsonPropertyName("volumetricHeight")]
    public double? VolumetricHeight { get; set; }
}