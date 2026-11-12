using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolRequestCreateTestQuestion
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    /// <summary>Content id of the product the question is asked about.</summary>
    [JsonPropertyName("contentId")]
    public long ContentId { get; set; }

    [JsonPropertyName("userId")]
    public long UserId { get; set; }

    [JsonPropertyName("userFullName")]
    public string UserFullName { get; set; }

    [JsonPropertyName("showUserName")]
    public bool ShowUserName { get; set; }

    /// <summary>1 can be used.</summary>
    [JsonPropertyName("channelId")]
    public int? ChannelId { get; set; }
}
