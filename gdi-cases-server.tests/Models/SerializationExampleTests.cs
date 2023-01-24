using System;
using DeepEqual.Syntax;
using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.tests.Models;

/// <summary>
/// Explorative serialization dumps
/// </summary>
[TestClass]
public class SerializationExampleTests: ModelTestBase
{
    [TestMethod]
    public void DumpXml()
    {
        var b = GenerateSampleBundle();
        // Console.WriteLine(ToXml(XmlDto.FromJsonDto(b)));
        Console.WriteLine(ToXml(b));
    }
    [TestMethod]
    public void DumpJson()
    {
        var b = GenerateSampleBundle();
        // Console.WriteLine(ToXml(XmlDto.FromJsonDto(b)));
        Console.WriteLine(ToXml(b));
    }
}
