using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolResponseGetCategoryAttributes
{
    [JsonPropertyName("categoryAttributes")]
    public List<TrendyolCategoryAttribute> CategoryAttributes { get; set; }
}