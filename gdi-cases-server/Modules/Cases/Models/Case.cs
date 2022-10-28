using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models;

[Description("A case")]
public class Case
{
    [Required, BsonElement("caseId"), StringLength(maximumLength: 24, MinimumLength = 8), Description("System specific Id of event")]
    public string CaseId { get; set; } = "";
    [Required, BsonElement("subjectId"), Description("Subject of event. Usually a person")]
    public string SubjectId { get; set; } = "";
    [Required, BsonElement("publisherStatus"), Description("System specific status of case")]
    public string PublisherStatus { get; set; } = "";
    [Required, BsonElement("status"), Description("Normalized status of case")]
    public string Status { get; set; } = "";
    [Required, BsonElement("updateTime"), Description("Time of update"), Range(typeof(DateTime), "1971-01-01", "3000-01-01")]
    public string UpdateTime { get; set; } = "";
    [BsonElement("label"), Description("Display label")]
    public string Label { get; set; } = "";
    [BsonElement("description"), Description("Display description")]
    public string Description { get; set; } = "";

    [BsonElement("events")]
    public IEnumerable<CaseEvent> Events { get; set; } = Enumerable.Empty<CaseEvent>();
    [BsonElement("actions")]
    public IEnumerable<CaseAction> Actions { get; set; } = Enumerable.Empty<CaseAction>();
}

