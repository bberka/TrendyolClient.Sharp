using System;

namespace TrendyolClient.Sharp.Exceptions;

internal abstract class TrendyolException : Exception
{
    protected TrendyolException(string message) : base(message)
    {
    }

    protected TrendyolException(string message, Exception innerException) : base(message, innerException)
    {
    }
}