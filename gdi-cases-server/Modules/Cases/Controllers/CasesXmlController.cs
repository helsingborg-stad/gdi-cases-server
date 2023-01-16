using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Json;
using gdi_cases_server.Modules.Cases.Models.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace gdi_cases_server.Modules.Cases.Controllers;

[ApiController]
[Route("/api/v1/xml/cases")]
public class CasesXmlController: Controller
{
    [Authorize]
    [HttpPut("upload", Name = "uploadXmlCasesOperation")]
    [Consumes("text/xml", "application/xml")]
    [Produces("text/xml", "application/xml")]
    public UploadCasesResult UploadCasesXml(Bundle bundle)
    {
        //Database.UpdateCases(bundle);
        return new UploadCasesResult();
    }

    [HttpGet("generate-sample-bundle", Name = "generateXmlSampleBundleOperation")]
    [Produces("text/xml", "application/xml")]
    public Bundle GenerateSampleBundle(string subjectId, int randomSeed = 0)
    {
        return XmlDto.FromJsonDto(new SampleDataGenerator().GenerateSampleBundle(subjectId, randomSeed));
    }
}
