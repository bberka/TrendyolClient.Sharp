using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when request validation fails
/// </summary>
internal class TrendyolValidationException : TrendyolException
{
    public TrendyolValidationException(IEnumerable<ValidationFailure> errors) : base("One or more validation errors occurred")
    {
        Errors = errors ?? [];
        ErrorsByProperty = Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage)
                .ToArray());
    }

    public TrendyolValidationException(string propertyName, string errorMessage) : base($"Validation failed for {propertyName}: {errorMessage}")
    {
        Errors =
        [
            new ValidationFailure(propertyName, errorMessage)
        ];
        ErrorsByProperty = new Dictionary<string, string[]>
        {
            [propertyName] = [errorMessage]
        };
    }

    /// <summary>
    ///     Collection of validation errors
    /// </summary>
    public IEnumerable<ValidationFailure> Errors { get; }

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