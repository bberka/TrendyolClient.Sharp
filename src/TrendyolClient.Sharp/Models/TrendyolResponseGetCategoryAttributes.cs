using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCategoryAttributes
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("categoryAttributes")]
    public List<TrendyolCategoryAttribute> CategoryAttributes { get; set; }
}
