using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolQuestion
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("questionText")]
    public string QuestionText { get; set; }

    [JsonPropertyName("createdDate")]
    public long CreatedDate { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}