using gdi_cases_server.Modules.Cases.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace gdi_cases_server.Modules.Cases.Controllers;

[ApiController]
[Route("/api/v1/cases")]
public class CasesController : Controller
{
    public CasesController(ICasesDatabase casesDatabase)
    {
        Database = casesDatabase;
    }

    public ICasesDatabase Database { get; }

    [HttpGet("get-constants", Name = "getConstantsOperation")]
    public ConstantsResult Constants()
    {
        return new ConstantsResult
        {
            StatusHints = Enum.GetNames<StatusHint>().Select(s => s.ToLower()),
            TypeHints = Enum.GetNames<ActionTypeHint>().Select(s => s.ToLower()),
        };
    }

    [HttpGet("list-cases-by-subject", Name = "listCasesBySubjectOperation")]
    [Authorize]
    public IEnumerable<Case> ListCasesBySubject(string subjectId) {
        return Database.ListCasesBySubject(subjectId);
    }

    [HttpPut("upload", Name = "uploadCasesOperation")]
    [Authorize]
    public UploadCasesResult UploadCases(CasesBundle bundle)
    {
        Database.UpdateCases(bundle);
        return new UploadCasesResult();
    }
}
