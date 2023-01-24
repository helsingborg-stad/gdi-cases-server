using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Json;
using gdi_cases_server.Modules.Cases.Models.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace gdi_cases_server.Modules.Cases.Controllers;

[ApiController]
[Route("/api/v1/cases/xml")]
[SwaggerTag("XML REST API")]
public class CasesXmlController: Controller
{
    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        return base.OnActionExecutionAsync(context, next);
    }

    [Authorize]
    [HttpPut("upload", Name = "uploadXmlCasesOperation")]
    [Consumes("application/xml")]
    [Produces("application/json", "application/xml")]
    public Bundle UploadCasesXml([FromBody] Bundle bundle)
    {
        if (ModelState.IsValid)
        {
            Console.Write(ModelState.IsValid);
        }
        return bundle;
    }
    /*
    public UploadCasesResult UploadCasesXml([FromBody] Bundle bundle)
    {
        //Database.UpdateCases(bundle);
        return new UploadCasesResult();
    }
    */

    [HttpGet("generate-sample-bundle", Name = "generateXmlSampleBundleOperation")]
    [Produces("text/xml", "application/xml")]
    public CasesBundleXmlDto GenerateSampleBundle(string subjectId, int randomSeed = 0)
    {
        return XmlDto.FromJsonDto(new SampleDataGenerator().GenerateSampleBundle(subjectId, randomSeed));
    }
}
