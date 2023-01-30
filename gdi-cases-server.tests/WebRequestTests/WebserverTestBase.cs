using Flurl.Http;
using Flurl.Http.Configuration;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace gdi_cases_server.tests.WebRequestTests;

public class WebserverTestBase : NiceToHaveTestBase
{
    public class JsonSerializerForFlurl : ISerializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        public T Deserialize<T>(string s) => JsonSerializer.Deserialize<T>(s, _options);
        public T Deserialize<T>(Stream stream) => JsonSerializer.Deserialize<T>(stream, _options);
        public string Serialize(object obj) => JsonSerializer.Serialize(obj, _options);
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


