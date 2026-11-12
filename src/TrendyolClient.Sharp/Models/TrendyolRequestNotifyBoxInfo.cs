using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestNotifyBoxInfo
{
    /// <summary>
    ///     Required for Horoz and CEVA.
    /// </summary>
    [JsonPropertyName("boxQuantity")]
    public int BoxQuantity { get; set; }

    /// <summary>
    ///     Required for Horoz and CEVA.
    /// </summary>
    [JsonPropertyName("deci")]
    public decimal Deci { get; set; }
}
