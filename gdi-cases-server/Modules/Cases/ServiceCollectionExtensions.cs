// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using gdi_cases_server.Modules.Cases.MongoDb;

namespace gdi_cases_server.Modules.Cases;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCasesDatabase(this IServiceCollection services)
    {
        services.AddSingleton<ICasesDatabase>(new MongoDbCasesDatabaseFactory().TryCreateDatabaseFromEnv() ?? MissingConfiguration<ICasesDatabase>("Database configuration is missing. Expected atleast MONGODB_URI=... in environment."));
        return services;
    }

    private static T MissingConfiguration<T>(string message)
    {
        throw new ApplicationException(message);
    }
}
