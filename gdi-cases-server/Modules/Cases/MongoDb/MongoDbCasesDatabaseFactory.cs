using MongoDB.Driver;

namespace gdi_cases_server.Modules.Cases.MongoDb;

public class MongoDbCasesDatabaseFactory
{
    private static string? GetEnv(string name, string? defaultValue = null) => new[] { Environment.GetEnvironmentVariable(name) }.Select(s => s?.Trim())
        .Where(s => !string.IsNullOrEmpty(s))
        .FirstOrDefault(defaultValue);

    public ICasesDatabase? TryCreateDatabaseFromEnv()
    {
        var session = MongoDbSession.TryCreate(
            GetEnv("MONGODB_URI", null),
            GetEnv("MONGODB_DATABASE", "cases"),
            GetEnv("MONGODB_COLLECTION", "cases")
            );
        return session != null ? new MongoDbCasesDatabase(session) : null;
    }

}

