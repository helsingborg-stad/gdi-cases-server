using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Cases;

public class CasesStats
{
    [BsonElement("count")]
    public int Count { get; set; }
    [BsonElement("markedAsReadCount")]
    public int MarkedAsReadCount { get; set; }
}
