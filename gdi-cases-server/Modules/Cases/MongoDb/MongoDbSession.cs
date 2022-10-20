using MongoDB.Driver;

namespace gdi_cases_server.Modules.Cases.MongoDb;

public class MongoDbSession {

    public MongoDbSession(MongoClient client, IMongoDatabase db, IMongoCollection<MongoDbCaseRecord> collection)
    {
        Client = client;
        Db = db;
        Collection = collection;
    }

    public MongoClient Client { get; }
    public IMongoDatabase Db { get; }
    public IMongoCollection<MongoDbCaseRecord> Collection { get; }

    public static MongoDbSession? TryCreate(string? uri, string? databaseName, string? collectionName)
    {
        if (new[] {uri, databaseName, collectionName}.Where(string.IsNullOrEmpty).Count() > 0)
        {
            return null;
        }

        var settings = MongoClientSettings.FromConnectionString(uri);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var db = client.GetDatabase(databaseName);
        
        // db.CreateCollection(collectionName);
        var collection = db.GetCollection<MongoDbCaseRecord>(collectionName);
        collection.Indexes.CreateOne(new CreateIndexModel<MongoDbCaseRecord>(new IndexKeysDefinitionBuilder<MongoDbCaseRecord>().Ascending(record => record.RecordId), new CreateIndexOptions { Unique = true }));
        collection.Indexes.CreateOne(new CreateIndexModel<MongoDbCaseRecord>(new IndexKeysDefinitionBuilder<MongoDbCaseRecord>().Ascending(record => record.SubjectId)));
        return new MongoDbSession(client, db, collection);
    }
}

