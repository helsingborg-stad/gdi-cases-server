using System.Text.Json;
using System.Text.Json.Serialization;

namespace gdi_cases_server.Converters;

public class RelaxedEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    static readonly Dictionary<string, T> NameToValue = Enum.GetValues<T>().ToDictionary(value => Enum.GetName<T>(value), value => value, StringComparer.OrdinalIgnoreCase);

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    {
                        var s = reader.GetString();
                        T v;
                        var r = NameToValue.TryGetValue(s, out v) ? v : default(T);
                        return r;
                    }
                    break;
                case JsonTokenType.Number:
                    {
                        var i = reader.GetInt32();
                        return Enum.GetValues<T>().FirstOrDefault(v => Convert.ToInt32(v) == i);
                    }
                    break;
            }
        }
        catch (System.FormatException) { }
        return default(T);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(Enum.GetName<T>(value)?.ToLower());
    }
}
