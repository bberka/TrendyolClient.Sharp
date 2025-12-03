using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace TrendyolClient.Sharp.Extensions;

internal static class TrendyolJsonExtensions
{
  private static readonly JsonSerializerSettings Settings = new() {
    ContractResolver = new DefaultContractResolver {
      NamingStrategy = new CamelCaseNamingStrategy()
    },
    TypeNameHandling = TypeNameHandling.Auto,
    NullValueHandling = NullValueHandling.Ignore,
    Converters = { new StringEnumConverter() },
    Formatting = Formatting.Indented
  };

  public static string ObjectToJsonString(this object obj) {
    return JsonConvert.SerializeObject(obj, Settings);
  }

  public static T JsonToObject<T>(this string json) {
    return JsonConvert.DeserializeObject<T>(json, Settings);
  }
}