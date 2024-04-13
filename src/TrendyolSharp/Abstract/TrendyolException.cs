﻿using System;

namespace TrendyolSharp.Abstract
{
  public abstract class TrendyolException : Exception
  {
    protected TrendyolException() { }

    protected TrendyolException(string message) : base(message) { }

    protected TrendyolException(string message, Exception innerException) : base(message, innerException) { }
  }
}