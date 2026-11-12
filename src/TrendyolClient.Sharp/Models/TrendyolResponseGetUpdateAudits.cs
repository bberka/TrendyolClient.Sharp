using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetUpdateAudits
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("totalPage")]
    public int TotalPage { get; set; }

    [JsonPropertyName("content")]
    public List<TrendyolUpdateAuditItem> Content { get; set; }
}
