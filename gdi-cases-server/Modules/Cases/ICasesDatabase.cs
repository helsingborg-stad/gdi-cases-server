using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Json;

namespace gdi_cases_server.Modules.Cases;

public interface ICasesDatabase {
    IEnumerable<CaseJsonDto> ListCasesBySubject(string subjectId);
    void UpdateCases(CasesBundleJsonDto bundle);
}
