using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportResponseGetCategoryAttributes
{
    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("categoryAttributes")]
    public List<TrendyolExportCategoryAttribute> CategoryAttributes { get; set; }
}
