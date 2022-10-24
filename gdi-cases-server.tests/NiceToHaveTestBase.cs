using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.tests;

public class NiceToHaveTestBase
{
    public CasesBundle CreateTestCaseBundle(int casesCount = 2)
    {
        return new CasesBundle
        {
            PublisherId = "test-publisher",
            SystemId = "test-systemid",
            Cases = Enumerable.Range(0, casesCount).Select(i => CreateTestCase(i))
        };


    }
    // Create a simple testcase
    public Case CreateTestCase(int seed) => new Case()
    {
        CaseId = $"test-case-{seed}",
        SubjectId = $"test-case-{seed}",
        PublisherStatus = $"test-case-{seed}",
        Status = $"test-case-{seed}",
        UpdateTime = $"test-case-{seed}",
        DisplayLabel = $"test-case-{seed}",
        Description = $"test-case-{seed}",
        Events = Enumerable.Empty<CaseEvent>(),
        Actions = Enumerable.Empty<CaseAction>()
    };

}
