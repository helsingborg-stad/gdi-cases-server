﻿using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;
using gdi_cases_server.Modules.Cases.Models.Constants;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    public Constants Constants()
    {
        return new Constants()
        {
            CaseActionTypeHints = Enumeration.FromEnum<ActionTypeHint>(),
            CaseStatusHints = Enumeration.FromEnum<StatusHint>()
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
    public Bundle UploadCases(Bundle bundle)
    {
        var normalizedBundle = bundle.Normalize();
        Database.UpdateCases(normalizedBundle);
        return normalizedBundle;
    }

    [HttpGet("generate-sample-bundle", Name = "generateSampleBundleOperation")]
    public Bundle GenerateSampleBundle(string subjectId, int randomSeed = 0)
    {
        return new SampleDataGenerator().GenerateSampleBundle(subjectId, randomSeed);
    }
}
