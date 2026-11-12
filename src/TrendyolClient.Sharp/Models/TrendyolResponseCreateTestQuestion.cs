using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseCreateTestQuestion
{
    [JsonPropertyName("questionId")]
    public long QuestionId { get; set; }
}
