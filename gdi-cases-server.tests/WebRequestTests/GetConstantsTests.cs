using Flurl.Http;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Constants;
using Moq;

namespace gdi_cases_server.tests.WebRequestTests;

[TestClass]
public class GetConstantsTests : WebserverTestBase
{
    [TestMethod]
    [Description("/api/v1/cases/get-constants is public")]
    public async Task RequiresNoAuthorization()
    {
        var apiKeys = new CasesApiKeys(key => false);
        var database = new Mock<ICasesDatabase>().Object;
        await WithWebServer(
            apiKeys,
            database,
            async client =>
            {
                var c = await Request(client, "/api/v1/cases/get-constants")
                    .WithHeader("Accept", "application/json")
                    .GetAsync()
                    .ReceiveJson<Constants>();

                Assert.IsNotNull(c);
            });
    }

    [TestMethod]
    [Description("/api/v1/cases/get-constants returns important enums")]
    public async Task ReturnWhatsExpected()
    {
        var apiKeys = new CasesApiKeys(key => true);
        var database = new Mock<ICasesDatabase>().Object;
        await WithWebServer(
            apiKeys,
            database,
            async client =>
            {
                var c = await Request(client, "/api/v1/cases/get-constants")
                        .WithHeader("Accept", "application/json")
                    .GetAsync()
                    .ReceiveJson<Constants>();

                CollectionAssert.AreEquivalent(
                    Enum.GetNames<ActionTypeHint>(),
                    c.CaseActionTypeHints.Select(h => h.Value).ToList());

                CollectionAssert.AreEquivalent(
                    Enum.GetNames<StatusHint>(),
                    c.CaseStatusHints.Select(h => h.Value).ToList());
            });
    }
}
