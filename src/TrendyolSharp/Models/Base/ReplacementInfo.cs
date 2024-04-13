﻿using System;
using System.Collections.Generic;

namespace TrendyolSharp.Models.Base
{
  public sealed class ReplacementInfo
  {
    public long CargoTrackingNumber { get; set; }
    public string CargoProviderName { get; set; }
    public string CargoSenderNumber { get; set; }
    public string CargoTrackingLink { get; set; }
    public long PackageId { get; set; }
    public List<Guid> Items { get; set; }
  }
}