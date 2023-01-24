using System.Text.Json;
using System.Text.Json.Serialization;

namespace gdi_cases_server.Converters;

public class StringValuesFromEnumConverter<T> : JsonConverter<string> where T : struct, Enum
{
    static readonly HashSet<string> Names = Enum.GetNames<T>().ToHashSet<string>(StringComparer.OrdinalIgnoreCase);

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(string);
    }

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var v = reader.GetString()?.ToLower();
            return Names.Contains(v) ? v : "";
        }
        return "";
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(
            Names.Contains(value) ? value.ToLower() : "");
    }
}
