using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models;

public class CasesBundle
{
    [Required, BsonElement("publisherId"), StringLength(maximumLength: 24, MinimumLength = 3), Description("Publisher Id. Usually an organisation or self governed unit.")]
    public string PublisherId { get; set; } = "";
    [Required, BsonElement("systemId"), StringLength(maximumLength: 24, MinimumLength = 3), Description("Distingushing Id of internal source of events within publisher")]
    public string SystemId { get; set; } = "";
    [BsonElement("cases")]
    public IEnumerable<Case>? Cases { get; set; } = Enumerable.Empty<Case>();
}

