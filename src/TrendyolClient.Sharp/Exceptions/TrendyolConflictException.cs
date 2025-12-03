namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when API request conflicts with current state
/// </summary>
internal class TrendyolConflictException(string message, string conflictDetails = null) : TrendyolException(message)
{
    /// <summary>
    ///     Additional details about the conflict
    /// </summary>
    public string ConflictDetails { get; } = conflictDetails;

    public override string ToString()
    {
        return string.IsNullOrEmpty(ConflictDetails) ? Message : $"{Message} - Details: {ConflictDetails}";
    }
}