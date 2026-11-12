using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolExportResponseGetCareInstructions
{
    [JsonPropertyName("items")]
    public List<TrendyolExportCareInstruction> Items { get; set; }
}
