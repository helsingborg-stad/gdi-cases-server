using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Json;
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

    [Authorize]
    [HttpGet("list-cases-by-subject", Name = "listCasesBySubjectOperation")]
    [Produces("text/json", "application/json")]
    public IEnumerable<CaseJsonDto> ListCasesBySubject(string subjectId) {
        return Database.ListCasesBySubject(subjectId);
    }

    [Authorize]
    [HttpPut("upload", Name = "uploadCasesOperation")]
    [Consumes("text/json", "application/json")]
    [Produces("text/json", "application/json")]
    public UploadCasesResult UploadCases(CasesBundleJsonDto bundle)
    {
        Database.UpdateCases(bundle);
        return new UploadCasesResult();
    }

    [HttpGet("generate-sample-bundle", Name = "generateSampleBundleOperation")]
    public CasesBundleJsonDto GenerateSampleBundle(string subjectId, int randomSeed = 0)
    {
        return new SampleDataGenerator().GenerateSampleBundle(subjectId, randomSeed);
    }
}
