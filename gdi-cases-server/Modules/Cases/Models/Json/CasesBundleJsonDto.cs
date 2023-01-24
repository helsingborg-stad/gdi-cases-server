using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Json;

public class Bundle
{
    [BsonElement("cases")]
    public List<Case>? Cases { get; set; } = new List<Case>();
}
