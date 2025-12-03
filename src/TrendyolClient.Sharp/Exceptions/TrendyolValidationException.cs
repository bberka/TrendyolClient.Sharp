using System.Collections.Generic;
using System.Linq;

namespace TrendyolClient.Sharp.Exceptions;

/// <summary>
///     Exception thrown when request validation fails
/// </summary>
internal class TrendyolValidationException(string propertyName, string errorMessage)
    : TrendyolException($"Validation failed for {propertyName}: {errorMessage}")
{
    
    
}