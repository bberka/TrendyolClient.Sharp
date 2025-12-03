namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when API returns an unexpected server error
/// </summary>
internal class TrendyolServerException(
    string message,
    int statusCode,
    string responseBody = null) : TrendyolException(message)
{
    /// <summary>
    ///     HTTP status code
    /// </summary>
    public int StatusCode { get; } = statusCode;

    /// <summary>
    ///     Response body from server
    /// </summary>
    public string ResponseBody { get; } = responseBody;

    public override string ToString()
    {
        return $"{Message} [StatusCode: {StatusCode}]";
    }
}