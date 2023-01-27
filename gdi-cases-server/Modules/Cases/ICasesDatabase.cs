using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;

namespace gdi_cases_server.Modules.Cases;

public interface ICasesDatabase {
    IEnumerable<Case> ListCasesBySubject(string subjectId);
    void UpdateCases(Bundle bundle);
}
