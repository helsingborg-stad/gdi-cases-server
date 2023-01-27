using DeepEqual.Syntax;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;

namespace gdi_cases_server.tests.Models;

[TestClass]
public class XmlSerializerTests: ModelTestBase {
    [TestMethod]
    public void XmlCanBeParsed () {
        var b = GenerateSampleBundle();
        FromXml<Bundle>(ToXml(b)).ShouldDeepEqual(b);
    }
}
