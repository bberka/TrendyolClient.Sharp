namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown for HTTP failures without a more specific exception type (for example 405, 414, 415).
/// </summary>
public class TrendyolRequestFailedException(string message, int statusCode = 0, string responseBody = null) : TrendyolException(message)
{
    /// <summary>
    ///     HTTP status code
    /// </summary>
    public int StatusCode { get; } = statusCode;

    /// <summary>
    ///     Response body from the server
    /// </summary>
    public string ResponseBody { get; } = responseBody;
}
