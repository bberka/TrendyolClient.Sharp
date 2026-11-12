using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Enums;

/// <summary>
///     Reads and writes enum members as upper snake case strings ("AtCollectionPoint" becomes "AT_COLLECTION_POINT").
/// </summary>
public sealed class TrendyolUpperSnakeCaseEnumConverter : JsonStringEnumConverter
{
    public TrendyolUpperSnakeCaseEnumConverter() : base(JsonNamingPolicy.SnakeCaseUpper)
    {
    }
}
