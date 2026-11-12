using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseAnswerQuestion
{
    [JsonPropertyName("answerId")]
    public long AnswerId { get; set; }
}
