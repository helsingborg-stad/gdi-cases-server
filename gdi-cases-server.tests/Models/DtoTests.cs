using System;
using DeepEqual.Syntax;
using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.tests.Models;

/// <summary>
/// Test converting between JsonDto stuff (for JSON API) and XmlDTO (from XML API)
/// </summary>
[TestClass]
public class DtoTests: ModelTestBase
{
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
}
