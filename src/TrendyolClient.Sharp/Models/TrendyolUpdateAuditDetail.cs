using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

public class TrendyolUpdateAuditDetail
{
    /// <summary>
    ///     TITLE, DESCRIPTION, MEDIA or ATTRIBUTE; decides which changed/existing fields are filled.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    ///     SUCCESS, FAIL or RUNNING.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("completedDate")]
    public string CompletedDate { get; set; }

    /// <summary>
    ///     Present when status is FAIL.
    /// </summary>
    [JsonPropertyName("rejectReasons")]
    public List<TrendyolUpdateAuditRejectReason> RejectReasons { get; set; }

    [JsonPropertyName("changedTitle")]
    public string ChangedTitle { get; set; }

    [JsonPropertyName("existingTitle")]
    public string ExistingTitle { get; set; }

    [JsonPropertyName("changedDescription")]
    public string ChangedDescription { get; set; }

    [JsonPropertyName("existingDescription")]
    public string ExistingDescription { get; set; }

    [JsonPropertyName("changedMedias")]
    public List<TrendyolUpdateAuditMedia> ChangedMedias { get; set; }

    [JsonPropertyName("existingMedias")]
    public List<TrendyolUpdateAuditMedia> ExistingMedias { get; set; }

    [JsonPropertyName("changedAttributes")]
    public List<TrendyolUpdateAuditChangedAttribute> ChangedAttributes { get; set; }

    [JsonPropertyName("existingAttributes")]
    public List<TrendyolUpdateAuditExistingAttribute> ExistingAttributes { get; set; }
}
