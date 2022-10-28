using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models;

[Description("A description of a milestone in a case process")]
public class CaseEvent
{
    [Required, BsonElement("updateTime")]
    public string UpdateTime { get; set; }
    [BsonElement("label"), Description("Display label")]
    public string Label { get; set; } = "";
    [BsonElement("description"), Description("Display description")]
    public string Description { get; set; } = "";
    [BsonElement("actions")]
    public IEnumerable<CaseAction> Actions { get; set; } = Enumerable.Empty<CaseAction>();
}

