using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseRequestBarcode
{
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; }
}