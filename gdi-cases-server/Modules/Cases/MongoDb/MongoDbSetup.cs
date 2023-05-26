using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace gdi_cases_server.Modules.Cases.MongoDb;

public class MongoDbSetup
{
    public static void Configure()
    {
        // Make sure we can serialize our own types
        var objectSerializer = new ObjectSerializer(
            type =>
            {
                return
                    ObjectSerializer.DefaultAllowedTypes(type)
                    || type.FullName.StartsWith("gdi_cases_server");
            });
        BsonSerializer.RegisterSerializer(objectSerializer);
    }
}

