using System.Text.Json;
using System.Xml.Serialization;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;

namespace gdi_cases_server.tests;

public class NiceToHaveTestBase
{
    public Bundle CreateTestCaseBundle(int casesCount = 2)
    {
        return new Bundle
        {
            Cases = Enumerable.Range(0, casesCount).Select(i => CreateTestCase(i)).ToList()
        };
    }

    // Create a simple testcase
    public Case CreateTestCase(int seed) => new Case()
    {
        PublisherId = "test-publisher",
        SystemId = "test-systemid",
        CaseId = $"test-case-{seed}",
        SubjectId = $"test-subject-{seed}",
        Status = $"test-status-{seed}",
        UpdateTime = FormatDate(DateTime.Now),
        Label = $"test-label-{seed}",
        Description = $"test-description-{seed}",
    };

    private static string FormatDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }


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

    public static string ToJson<T>(T value) => JsonSerializer.Serialize<T>(value, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

    public static T? FromJson<T>(string json) where T : class => JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

    public static T? ToAndFromJSON<T>(object v) where T : class => FromJson<T>(ToJson(v));
}
