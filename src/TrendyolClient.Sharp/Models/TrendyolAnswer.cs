using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolAnswer
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("answerText")]
    public string AnswerText { get; set; }

    [JsonPropertyName("createdDate")]
    public long CreatedDate { get; set; }
}