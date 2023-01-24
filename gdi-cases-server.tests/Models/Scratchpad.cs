using System.Text.Json;
using Flurl.Http;
using Flurl.Http.Configuration;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Controllers;
using Moq;

namespace gdi_cases_server.tests.Models
{
    [TestClass]
	public class Scratchpad: WebserverTestBase
    {
        [TestMethod]
		public async Task X()
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
//					.WithHeader("Authorization", "api-key test")
					.GetAsync()
					.ReceiveJson<ConstantsResult>();

				Console.Write(c);
			});
        }
	}
}

