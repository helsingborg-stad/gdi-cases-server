using System.Text.Json;
using Flurl.Http;
using Flurl.Http.Configuration;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using static gdi_cases_server.tests.Models.Scratchpad;

namespace gdi_cases_server.tests;

public class WebserverTestBase
{
    public class JsonSerializerForFlurl : ISerializer
    {
        public T Deserialize<T>(string s) => JsonSerializer.Deserialize<T>(s);
        public T Deserialize<T>(Stream stream) => JsonSerializer.Deserialize<T>(stream);
        public string Serialize(object obj) => JsonSerializer.Serialize(obj);
    }

    public IHostBuilder CreateHostBuilder(ICasesApiKeys apiKeys, ICasesDatabase database) =>
        Program
            .CreateHostBuilder(apiKeys, database, new string[0])
            .ConfigureWebHost(webHostBuilder => webHostBuilder.UseTestServer());

    public async Task WithHost(ICasesApiKeys apiKeys, ICasesDatabase database, Func<IHost, Task> action)
    {
        using (var host = await CreateHostBuilder(apiKeys, database).StartAsync())
        {
            await action(host);
        }
    }

    public async Task WithWebServer(
        ICasesApiKeys apiKeys,
        ICasesDatabase database,
        Func<HttpClient, Task> action
        ) =>
        await WithHost(apiKeys, database, async host => await action(host.GetTestClient()));

    public IFlurlRequest Request(HttpClient client, params object[] urlSegments)
    {
        var request = new FlurlClient(client).Request(urlSegments);
        request.Settings.JsonSerializer = new JsonSerializerForFlurl();
        return request;
    }
}
