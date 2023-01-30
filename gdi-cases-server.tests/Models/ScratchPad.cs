using gdi_cases_server.Modules.Cases.Models.Constants;

namespace gdi_cases_server.tests.Models;

[TestClass]
public class ScratchPad: ModelTestBase {

    [TestMethod]
    public void X() {
        var c = new Constants();

        Console.WriteLine(ToJson(c));
    }
}