using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace gdi_cases_server.Modules.Cases.Controllers;

[ApiController]
[Route("/api/v1/cases")]
[SwaggerTag("JSON REST API")]
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
            StatusHints = Enum.GetNames<StatusHint>().Select(s => s.ToLower()).ToList(),
            TypeHints = Enum.GetNames<ActionTypeHint>().Select(s => s.ToLower()).ToList(),
        };
    }

    [Authorize]
    [HttpGet("list-cases-by-subject", Name = "listCasesBySubjectOperation")]
    [Produces("text/json", "application/json")]
    public IEnumerable<Case> ListCasesBySubject(string subjectId) {
        return Database.ListCasesBySubject(subjectId);
    }

    [Authorize]
    [HttpPut("upload", Name = "uploadCasesOperation")]
    //[Consumes("text/json", "application/json", "text/xml", "application/xml")]
    //[Produces("text/json", "application/json", "text/xml", "application/xml")]
    public Bundle UploadCases(Bundle bundle)
    {
        // Database.UpdateCases(bundle);
        return bundle;
    }

    [HttpGet("generate-sample-bundle", Name = "generateSampleBundleOperation")]
    public Bundle GenerateSampleBundle(string subjectId, int randomSeed = 0)
    {
        return new SampleDataGenerator().GenerateSampleBundle(subjectId, randomSeed);
    }
}
