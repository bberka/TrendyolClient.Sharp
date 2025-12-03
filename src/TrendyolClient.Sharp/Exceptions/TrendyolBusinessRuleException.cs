using System.Collections.Generic;

namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when a business rule validation fails on the server
/// </summary>
internal class TrendyolBusinessRuleException(
    string message,
    string errorCode = null,
    Dictionary<string, object> errorDetails = null) : TrendyolException(message)
{
    /// <summary>
    ///     Error code from Trendyol API
    /// </summary>
    public string ErrorCode { get; } = errorCode;

    /// <summary>
    ///     Additional error details from API
    /// </summary>
    public Dictionary<string, object> ErrorDetails { get; } = errorDetails ?? new Dictionary<string, object>();

    public override string ToString()
    {
        var codeInfo = !string.IsNullOrEmpty(ErrorCode) ? $" [Code: {ErrorCode}]" : "";
        return $"{Message}{codeInfo}";
    }
}