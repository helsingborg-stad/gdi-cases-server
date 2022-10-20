using gdi_cases_server.Modules.Cases.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.MongoDb;

public class MongoDbCaseRecord
{
    [BsonElement("recordId")]
    public string RecordId { get; set; }
    [BsonElement("subjectId")]
    public string SubjectId { get; set; }
    [BsonElement("case")]
    public Case Case { get; set; }

}

