using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>Body of updateBoxInfo (International).</summary>
public class TrendyolIntlRequestNotifyBoxInfo
{
    /// <summary>Number of boxes.</summary>
    [JsonPropertyName("boxQuantity")]
    public int BoxQuantity { get; set; }

    /// <summary>Volumetric weight: width x length x height / 5000.</summary>
    [JsonPropertyName("deci")]
    public decimal Deci { get; set; }

    /// <summary>Centimetres. Mandatory for the CEE region only (1-210).</summary>
    [JsonPropertyName("width")]
    public decimal? Width { get; set; }

    /// <summary>Centimetres. Mandatory for the CEE region only (1-400).</summary>
    [JsonPropertyName("length")]
    public decimal? Length { get; set; }

    /// <summary>Centimetres. Mandatory for the CEE region only (1-200).</summary>
    [JsonPropertyName("height")]
    public decimal? Height { get; set; }
}
