using Flurl.Http;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Models.Cases;
using Moq;

namespace gdi_cases_server.tests.WebRequestTests;

[TestClass]
public class UploadCasesTests: WebserverTestBase
{
    [TestMethod]
    public Task RespondsWith200WhenAuthorizedAndValidated() => Put("/api/v1/cases/upload", XmlContentFrom(new Bundle()))
        .UseApiKeys(key => key == "test-api-key")
        .UseDatabase(new Mock<ICasesDatabase>().Object)
        .SetupRequest(r => r.WithHeader("Authorization", "Bearer test-api-key"))
        .Send(r => Assert.AreEqual(200, r.StatusCode));


    [TestMethod]
    public Task RespondsWith401WhenUnauthorized() => Put("/api/v1/cases/upload", XmlContentFrom(new Bundle()))
        .UseApiKeys(key => false)
        .UseDatabase(new Mock<ICasesDatabase>().Object)
        .Send(r => Assert.AreEqual(401, r.StatusCode));
}
