using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models;

public class CasesBundle
{
    [BsonElement("cases")]
    public IEnumerable<Case>? Cases { get; set; } = Enumerable.Empty<Case>();
}

