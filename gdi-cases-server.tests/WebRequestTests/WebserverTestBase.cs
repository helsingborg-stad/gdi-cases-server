using Flurl.Http;
using Flurl.Http.Configuration;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Models.Cases;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Net.Http.Headers;
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

    public async Task<T> WithHost<T>(ICasesApiKeys apiKeys, ICasesDatabase database, Func<IHost, Task<T>> action)
    {
        using (var host = await CreateHostBuilder(apiKeys, database).StartAsync())
        {
            return await action(host);
        }
    }

    public Task<T> WithWebServer<T>(
        ICasesApiKeys apiKeys,
        ICasesDatabase database,
        Func<HttpClient, Task<T>> action
        ) => WithHost<T>(apiKeys, database, async host => await action(host.GetTestClient()));

    public Task<T> WithWebServer<T>(Func<HttpClient, Task<T>> action) => WithWebServer<T>(
        new CasesApiKeys(key => false),
        new Mock<ICasesDatabase>().Object,
        action);

    public IFlurlRequest Request(HttpClient client, params object[] urlSegments)
    {
        var request = new FlurlClient(client).Request(urlSegments);
        request.Settings.JsonSerializer = new JsonSerializerForFlurl();
        return request;
    }

    public HttpContent XmlContentFrom<T>(T value) => new StringContent(ToXml(value), new MediaTypeHeaderValue("text/xml"));

    public class WrappedCall
    {
        public WebserverTestBase Owner { get; }
        public Func<string, bool> ValidateKey { get; set; }
        public ICasesDatabase Database { get; set; }
        public HttpMethod Method { get; set; }
        public HttpContent Content { get; set; }
        public string Path { get; set; }
        public List<Func<IFlurlRequest, IFlurlRequest>> SetupRequestActions { get; } = new List<Func<IFlurlRequest, IFlurlRequest>>();

        public WrappedCall(WebserverTestBase owner, HttpMethod method, HttpContent content, string path)
        {
            Owner = owner;
            Method = method;
            Content = content;
            Path = path;
            UseApiKeys(key => false);
            UseDatabase(null);
        }

        public WrappedCall Use(Action<WrappedCall> usage)
        {
            usage(this);
            return this;
        }
        public WrappedCall UseApiKeys(Func<string, bool> validateKey) => Use(wc => wc.ValidateKey = validateKey);
        public WrappedCall UseDatabase(ICasesDatabase db) => Use(wc => wc.Database = db);
        public WrappedCall SetupRequest(Func<IFlurlRequest, IFlurlRequest> setup) => Use(wc => wc.SetupRequestActions.Add(setup));

        public Task<IFlurlResponse> Send(params Action<IFlurlResponse>[] responseActions) => Owner.WithWebServer(
            new CasesApiKeys(ValidateKey),
            Database,
            async client =>
            {
                var request = Owner.Request(client, Path)
                    .WithHeader("Accept", "application/json")
                    .AllowAnyHttpStatus();
                foreach (var setup in SetupRequestActions)
                {
                    request = setup(request);
                }
                var response = await request.SendAsync(Method, Content);

                foreach (var responseAction in responseActions)
                {
                    responseAction(response);
                }
                return response;
            });
    }

    public WrappedCall Send(HttpMethod method, HttpContent content, string path) => new WrappedCall(this, method, content, path);
    public WrappedCall Get(string path) => Send(HttpMethod.Get, null, path);
    public WrappedCall Post(string path, HttpContent content) => Send(HttpMethod.Post, content, path);
    public WrappedCall Put(string path, HttpContent content) => Send(HttpMethod.Put, content, path);

}


