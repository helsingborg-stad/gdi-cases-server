using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using dotenv.net;
using gdi_cases_server.Authentication;
using gdi_cases_server.Modules.Cases;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.MongoDb;
using gdi_cases_server.XmlSupport;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class Program
{
    public static void Main(string[] args)
    {
        // Load configuration
        DotEnv.Load();

        // Setup configuration dependent services
        var apiKeys = CasesApiKeys.TryCreateFromEnv()
            ?? MissingConfiguration<ICasesApiKeys>("API key configuration is missing. Expected atleast API_KEY=... in environment.");
        var database = new MongoDbCasesDatabaseFactory().TryCreateDatabaseFromEnv()
            ?? MissingConfiguration<ICasesDatabase>("Database configuration is missing. Expected atleast MONGODB_URI=... in environment.");

        // Run web application
        CreateHostBuilder(apiKeys, database, args)
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(
        ICasesApiKeys apiKeys,
        ICasesDatabase database,
        params string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .ConfigureServices(services => services.AddSingleton<ICasesDatabase>(database))
            .ConfigureServices(services => services.AddSingleton<ICasesApiKeys>(apiKeys));

    private static T MissingConfiguration<T>(string message) => throw new ApplicationException(message);
}
