using DeepEqual.Syntax;
using gdi_cases_server.Modules.Cases.Models.Json;

namespace gdi_cases_server.tests.Models;

[TestClass]
public class JsonSerializerTests: ModelTestBase
{
    [TestMethod]
    public void JsonCanBeParsed()
    {
        var b = GenerateSampleBundle();
        FromJson<Bundle>(ToJson(b)).ShouldDeepEqual(b);
    }
}