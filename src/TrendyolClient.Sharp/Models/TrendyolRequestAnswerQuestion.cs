using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestAnswerQuestion
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}