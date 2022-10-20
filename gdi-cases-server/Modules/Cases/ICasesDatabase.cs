using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.Modules.Cases;

public interface ICasesDatabase {
    void UpdateCases(CasesBundle bundle);
}
