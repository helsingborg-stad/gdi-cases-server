using gdi_cases_server.Modules.Cases.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.MongoDb;

[BsonIgnoreExtraElements]
public class MongoDbCaseRecord
{
    [BsonElement("recordId")]
    public string RecordId { get; set; } = "";
    [BsonElement("subjectId")]
    public string SubjectId { get; set; } = "";
    [BsonElement("schemaVersion")]
    public string SchemaVersion { get; set; } = "1.0";
    [BsonElement("updateTime")]
    public DateTime UpdateTime { get; set; } = DateTime.Now;
    [BsonElement("case")]
    public Case? Case { get; set; }
}

