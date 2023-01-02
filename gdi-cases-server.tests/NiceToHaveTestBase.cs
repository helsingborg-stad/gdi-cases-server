using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.tests;

public class NiceToHaveTestBase
{
    public CasesBundle CreateTestCaseBundle(int casesCount = 2)
    {
        return new CasesBundle
        {
            Cases = Enumerable.Range(0, casesCount).Select(i => CreateTestCase(i))
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
        Events = Enumerable.Empty<CaseEvent>(),
        Actions = Enumerable.Empty<CaseAction>()
    };

    private static string FormatDate(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }

}
