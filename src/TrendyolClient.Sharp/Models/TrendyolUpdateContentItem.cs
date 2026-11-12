using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Item of an approved product content bulk update. Partial update is supported except for attributes.
/// </summary>
public class TrendyolUpdateContentItem
{
    [JsonPropertyName("contentId")]
    public long ContentId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("images")]
    public List<TrendyolProductImage> Images { get; set; }

    /// <summary>
    ///     When any attribute is updated, all attributes of the content must be sent. Slicer/varianter values cannot change.
    /// </summary>
    [JsonPropertyName("attributes")]
    public List<TrendyolProductAttribute> Attributes { get; set; }
}
