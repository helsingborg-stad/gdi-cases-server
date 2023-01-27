using DeepEqual.Syntax;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Controllers;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;
using gdi_cases_server.Modules.Cases.Models.Normalization;

namespace gdi_cases_server.tests;

[TestClass]
public class CaseControllerTests: NiceToHaveTestBase
{
    [TestMethod, Description("CaseController submits bundles to its database")]
    public void UploadCasesSubmitsBundleToDatabase()
    {
        var actualSumittedBundles = new List<Bundle>();

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
        Assert.IsTrue(testBundle.IsDeepEqual(actualSumittedBundles[0]));
    }

    [TestMethod]
    public void UploadCasesSubmitsNormalizedBundleToDatabase()
    {
        var bundle = new Bundle
        {
            Cases = new List<Case>() {
                new Case()
                {
                    PublisherId = "\tpublisherId\t",
                    CaseId = "    caseid    ",
                    SystemId = "\r\n systemId\r\n   ",
                    SubjectId = " a subject of some kind "
                }
            }
        };

        // Create a controller, relying on our database so we can intercept
        // and assert on expected calls
        var actualSumittedBundles = new List<Bundle>();

        var controller = new CasesController(new FakeDatabase()
        {
            OnUpdateCases = bundle => actualSumittedBundles.Add(bundle)
        });

        // Simulate REST call
        controller.UploadCases(bundle);

        Assert.AreEqual(1, actualSumittedBundles.Count);
        Assert.IsTrue(
            bundle.Normalize()
            .IsDeepEqual(actualSumittedBundles[0]));
    }

    // Simple fake instead of mock
    public class FakeDatabase : ICasesDatabase
    {
        public Action<Bundle>? OnUpdateCases { get; set; }

        public IEnumerable<Case> ListCasesBySubject(string subjectId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCases(Bundle bundle) => OnUpdateCases?.Invoke(bundle);
    }
}
