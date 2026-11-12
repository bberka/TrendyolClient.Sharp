using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;
using TrendyolClient.Sharp.Enums;

namespace TrendyolClient.Sharp;

/// <summary>
///     The JSON options every client uses: Refit's defaults (camelCase, case-insensitive, camelCase string enums) plus support for
///     enums that declare their own wire format.
/// </summary>
internal static class TrendyolJson
{
    public static JsonSerializerOptions CreateOptions()
    {
        var options = SystemTextJsonContentSerializer.GetDefaultJsonSerializerOptions();

        // Unset optional properties are left out of request bodies instead of being sent as null, so partial updates
        // (for example a price-only change) do not touch the other fields.
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        // Refit registers a global camelCase JsonStringEnumConverter, and options-level converters take precedence over the
        // [JsonConverter] attribute on an enum type. Ours must come first, or UPPER_SNAKE_CASE enums would be sent as "createdPackage".
        options.Converters.Insert(0, new UpperSnakeCaseEnumFactory());
        return options;
    }

    public static IHttpContentSerializer CreateContentSerializer()
    {
        return new SystemTextJsonContentSerializer(CreateOptions());
    }

    private sealed class UpperSnakeCaseEnumFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            var type = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;
            return type.IsEnum
                   && type.GetCustomAttribute<JsonConverterAttribute>()?.ConverterType == typeof(TrendyolUpperSnakeCaseEnumConverter);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new TrendyolUpperSnakeCaseEnumConverter().CreateConverter(typeToConvert, options);
        }
    }
}
