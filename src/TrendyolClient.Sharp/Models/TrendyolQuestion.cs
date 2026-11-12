using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolQuestion
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("customerId")]
    public long CustomerId { get; set; }

    /// <summary>Empty when showUserName is false.</summary>
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [JsonPropertyName("showUserName")]
    public bool ShowUserName { get; set; }

    /// <summary>WAITING_FOR_ANSWER, ANSWERED, REPORTED, REJECTED or UNANSWERED.</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("public")]
    public bool IsPublic { get; set; }

    /// <summary>Timestamp (ms).</summary>
    [JsonPropertyName("creationDate")]
    public long CreationDate { get; set; }

    /// <summary>Rejection reason, if any.</summary>
    [JsonPropertyName("reason")]
    public string Reason { get; set; }

    [JsonPropertyName("reportReason")]
    public string ReportReason { get; set; }

    [JsonPropertyName("reportedDate")]
    public long? ReportedDate { get; set; }

    [JsonPropertyName("rejectedDate")]
    public long? RejectedDate { get; set; }

    [JsonPropertyName("answeredDateMessage")]
    public string AnsweredDateMessage { get; set; }

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("productMainId")]
    public string ProductMainId { get; set; }

    [JsonPropertyName("webUrl")]
    public string WebUrl { get; set; }

    /// <summary>Active answer, if any.</summary>
    [JsonPropertyName("answer")]
    public TrendyolAnswer Answer { get; set; }

    /// <summary>Most recently rejected answer, if any.</summary>
    [JsonPropertyName("rejectedAnswer")]
    public TrendyolAnswer RejectedAnswer { get; set; }
}
