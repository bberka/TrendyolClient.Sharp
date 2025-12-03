using System;

namespace TrendyolClient.Sharp.Utils;

internal static class UtilDate
{
  public static long ToTimestampMiliseconds(this DateTime dateTime) {
    var unixEpoch = new DateTime(1970,
      1,
      1,
      0,
      0,
      0,
      DateTimeKind.Utc);
    var timeSpan = dateTime.ToUniversalTime() - unixEpoch;
    return (long)timeSpan.TotalMilliseconds;
  }
}