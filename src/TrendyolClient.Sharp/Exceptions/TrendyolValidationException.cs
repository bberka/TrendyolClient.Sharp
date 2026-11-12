using System.Collections.Generic;
using System.Linq;

namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown (with <see cref="TrendyolClientConfig.ThrowOnError" />) when Trendyol rejects a request as invalid (HTTP 400).
///     The library itself does not validate input; the reasons come from Trendyol's response.
/// </summary>
public class TrendyolValidationException : TrendyolException
{
    public TrendyolValidationException(IEnumerable<TrendyolValidationError> errors) : base("One or more validation errors occurred")
    {
        Errors = errors?.ToList() ?? [];
        ErrorsByProperty = Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage)
                .ToArray());
    }

    public TrendyolValidationException(string propertyName, string errorMessage) : base($"Validation failed for {propertyName}: {errorMessage}")
    {
        Errors = [new TrendyolValidationError(propertyName, errorMessage)];
        ErrorsByProperty = new Dictionary<string, string[]>
        {
            [propertyName] = [errorMessage]
        };
    }

    /// <summary>
    ///     Collection of validation errors
    /// </summary>
    public IReadOnlyList<TrendyolValidationError> Errors { get; }

    /// <summary>
    ///     Dictionary of errors grouped by property name
    /// </summary>
    public Dictionary<string, string[]> ErrorsByProperty { get; }

    public override string ToString()
    {
        var errors = string.Join("; ", Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
        return $"{Message} - {errors}";
    }
}

/// <summary>
///     One rejected property and the reason Trendyol gave.
/// </summary>
public sealed record TrendyolValidationError(string PropertyName, string ErrorMessage);
