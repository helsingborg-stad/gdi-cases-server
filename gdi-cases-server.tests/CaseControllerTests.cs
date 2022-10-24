using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Controllers;
using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.tests;

[TestClass]
public class CaseControllerTests: NiceToHaveTestBase
{
    [TestMethod, Description("CaseController submits bundles to its database")]
    public void UploadCasesSubmitsBundleToDatabase()
    {
        var actualSumittedBundles = new List<CasesBundle>();

        // Bundle to send as update to controller
        var testBundle = CreateTestCaseBundle(2);

        // Create a controller, relying on our database so we can intercept
        // and assert on expected calls
        var controller = new CasesController(new FakeDatabase()
        {
            OnUpdateCases = bundle => actualSumittedBundles.Add(bundle)
        });

        // Simulate REST call
        controller.UploadCases(testBundle);

        Assert.AreEqual(1, actualSumittedBundles.Count);
        Assert.AreSame(testBundle, actualSumittedBundles[0]);
    }

    // Simple fake instead of mock
    public class FakeDatabase : ICasesDatabase
    {
        public Action<CasesBundle>? OnUpdateCases { get; set; }
        public void UpdateCases(CasesBundle bundle) => OnUpdateCases?.Invoke(bundle);
    }
}
