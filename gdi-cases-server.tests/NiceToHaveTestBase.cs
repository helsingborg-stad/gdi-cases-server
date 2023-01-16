using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Json;

namespace gdi_cases_server.tests;

public class NiceToHaveTestBase
{
    public CasesBundleJsonDto CreateTestCaseBundle(int casesCount = 2)
    {
        return new CasesBundleJsonDto
        {
            Cases = Enumerable.Range(0, casesCount).Select(i => CreateTestCase(i)).ToList()
        };


    }

    // Create a simple testcase
    public CaseJsonDto CreateTestCase(int seed) => new CaseJsonDto()
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

}
