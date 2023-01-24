using System.Text.Json;
using System.Xml.Serialization;
using DeepEqual.Syntax;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Json;
using gdi_cases_server.Modules.Cases.Models.Xml;

namespace gdi_cases_server.tests;


[TestClass]
public class SerializationTests
{
    public static Bundle GenerateSampleBundle (string subject = "test-subject", int randomSeed = 0) => new SampleDataGenerator().GenerateSampleBundle(subject, randomSeed);


    [TestMethod]
    public void FromJsonDto()
    {
        // The XmlDto and JsonDto classes are used to convert between similar represenations
        var b = GenerateSampleBundle();

        XmlDto.FromJsonDto(b).ShouldDeepEqual(b);
    }

    [TestMethod]
    public void FromXmlDto()
    {
        var b = GenerateSampleBundle();
        JsonDto.FromXmlDto(XmlDto.FromJsonDto(b)).ShouldDeepEqual(b);
    }

    [TestMethod]
    public void Foo()
    {
        var b = GenerateSampleBundle();

        // Convert from JsonDto to XmlDto and the generate XML
        var xml = ToXml(XmlDto.FromJsonDto(b));

        var parsedFromXml = FromXml<CasesBundleXmlDto>(xml);

        parsedFromXml.ShouldDeepEqual(b);
    }



    public static T? FromXml<T>(string text) where T: class
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
    public static string ToJson<T>(T value)
    {
        return JsonSerializer.Serialize<T>(value, new JsonSerializerOptions { WriteIndented = true, });
    }
    [TestMethod]
    public void Test ()
    {
        var y = ToXml(XmlDto.FromJsonDto(new SampleDataGenerator().GenerateSampleBundle("123")));
        Console.WriteLine(y);


        var x = FromXml<CasesBundleXmlDto>("<Bundle><Cases><Case/></Cases></Bundle>");
        Console.Write(ToJson(x));

        // new XmlSerializer(typeof(XmlCasesBundle)).Deserialize()
    }
}
