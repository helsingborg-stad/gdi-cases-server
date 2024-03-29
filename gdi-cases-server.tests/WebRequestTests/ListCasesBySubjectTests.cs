﻿using Flurl.Http;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Models.Cases;
using Moq;

namespace gdi_cases_server.tests.WebRequestTests;

[TestClass]
public class ListCasesBySubjectTests : WebserverTestBase
{

    [TestMethod]
    [Description("/api/v1/cases/list-cases-by-subject requires authorization")]
    public Task RequiresAuthorization() => Get("/api/v1/cases/list-cases-by-subject")
            .UseApiKeys(key => false)
            .UseDatabase(new Mock<ICasesDatabase>().Object)
            .Send(r => Assert.AreEqual(401, r.StatusCode));

    [TestMethod]
    [Description("/api/v1/cases/list-cases-by-subject can be authorized")]
    public async Task AllowsAuthorizedCalls()
    {
        // Prepare for authorization and a cases response
        var testApiKey = "test-api-key";
        var testSubjectId = "test-api-key";
        var fakeCases = new List<AnnotatedCase>() { new AnnotatedCase { CaseId = "testCaseId" } };
        var apiKeys = new CasesApiKeys(key => key == testApiKey);
        var databaseMock = new Mock<ICasesDatabase>();
        databaseMock.Setup(db => db
            .ListCasesBySubject(testSubjectId))
            .Returns(() => fakeCases);

        var database = databaseMock.Object;

        // start webserver
        await WithWebServer(
            apiKeys,
            database,
            async client =>
            {
                // perform authorized request
                var cases = await Request(client, $"/api/v1/cases/list-cases-by-subject?subjectId={testSubjectId}")
                    .WithHeader("Accept", "application/json")
                    .WithHeader("Authorization", "Bearer test-api-key")
                    .GetAsync()
                    .ReceiveJson<List<Case>>();

                // enjoy the cases
                Assert.IsNotNull(cases);
                Assert.AreEqual(1, cases.Count);
                Assert.AreEqual("testCaseId", cases[0].CaseId);
                return true;
            });
    }
}
