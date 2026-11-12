using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrendyolClient.Sharp.Models;

/// <summary>
///     Reads a JSON array of strings, treating a bare string (including the empty string Trendyol sends
///     when there is nothing to report) as a one-element list, or an empty list for "".
/// </summary>
public sealed class TrendyolStringListConverter : JsonConverter<List<string>>
{
    public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.StartArray:
                var list = new List<string>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    list.Add(reader.TokenType == JsonTokenType.String ? reader.GetString() : null);
                }

                return list;
            case JsonTokenType.String:
                var value = reader.GetString();
                return string.IsNullOrEmpty(value) ? new List<string>() : new List<string> { value };
            default:
                return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var item in value)
        {
            writer.WriteStringValue(item);
        }

        writer.WriteEndArray();
    }
}
