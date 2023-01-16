using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Json;

public class CasesBundleJsonDto
{
    [BsonElement("cases")]
    public List<CaseJsonDto>? Cases { get; set; } = new List<CaseJsonDto>();
}
