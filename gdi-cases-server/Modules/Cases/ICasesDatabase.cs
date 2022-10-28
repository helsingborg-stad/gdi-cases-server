using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.Modules.Cases;

public interface ICasesDatabase {
    IEnumerable<Case> ListCasesBySubject(string subjectId);
    void UpdateCases(CasesBundle bundle);
}
