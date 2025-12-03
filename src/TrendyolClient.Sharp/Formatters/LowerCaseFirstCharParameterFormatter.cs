using System.Web;
using Refit;

namespace TrendyolClient.Sharp.Formatters;

internal sealed class LowerCaseFirstCharParameterFormatter : IUrlParameterKeyFormatter
{
    public string Format(string key)
    {
        var formattedValue = char.ToLowerInvariant(key[0]) + key[1..];
        return HttpUtility.UrlEncode(formattedValue);
    }
}