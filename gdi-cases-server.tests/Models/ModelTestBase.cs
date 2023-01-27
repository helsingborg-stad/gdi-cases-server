using System.Text.Json;
using System.Xml.Serialization;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;

namespace gdi_cases_server.tests.Models;

public class ModelTestBase {
    public static Bundle GenerateSampleBundle(string subject = "test-subject", int randomSeed = 0) => new SampleDataGenerator().GenerateSampleBundle(subject, randomSeed);

    public static T? FromXml<T>(string text) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));
        return serializer.Deserialize(new StringReader(text)) as T;
    }

    public static string ToXml<T>(T value)
    {
        var serializer = new XmlSerializer(typeof(T));
        var sw = new StringWriter();
        serializer.Serialize(sw, value);
        return sw.ToString();
    }

    public static string ToJson<T>(T value) => JsonSerializer.Serialize<T>(value, new JsonSerializerOptions { WriteIndented = true });

    public static T? FromJson<T>(string json) where T: class => JsonSerializer.Deserialize<T>(json);

    public static T? ToAndFromJSON<T>(object v) where T: class => FromJson<T>(ToJson(v));
}
