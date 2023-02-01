using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using gdi_cases_server.Modules.Cases.Models.Validators;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Cases;

[Description("A case")]
public class Case: INormalizable<Case>
{
    [Required, BsonElement("publisherId"), StringLength(maximumLength: 128, MinimumLength = 3), Description("Publisher Id. Usually an organisation or self governed unit.")]
    [Id]
    public string PublisherId { get; set; } = "";

    [Required, BsonElement("systemId"), StringLength(maximumLength: 128, MinimumLength = 3), Description("Distingushing Id of internal source of events within publisher")]
    [Id]
    public string SystemId { get; set; } = "";

    [Required, BsonElement("caseId"), StringLength(maximumLength: 128, MinimumLength = 8), Description("System specific Id of event")]
    [Id]
    public string CaseId { get; set; } = "";

    [Required, BsonElement("subjectId"), Description("Subject of event. Usually a person")]
    [Id]
    public string SubjectId { get; set; } = "";

    [Required, BsonElement("updateTime"), Description("Time of update"), Range(typeof(DateTime), "1971-01-01", "3000-01-01")]
    public string UpdateTime { get; set; } = "";

    [BsonElement("expires"), Description("Time of expiration"), Range(typeof(DateTime), "1971-01-01", "3000-01-01")]
    public string Expires { get; set; } = "";

    [BsonElement("organization"), Description("Organization name")]
    public string Organization { get; set; } = "";

    [BsonElement("label"), Description("Display label")]
    public string Label { get; set; } = "";

    [BsonElement("description"), Description("Display description")]
    public string Description { get; set; } = "";

    [BsonElement("status"), Description("Display status")]
    public string Status { get; set; } = "";

    [BsonElement("statusHint"), Description("Status hint")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<StatusHint>))]
    public string StatusHint { get; set; } = "";

    [BsonIgnore, Description("Deleted flag")]
    public bool IsDeleted { get; set; } = false;

    [BsonElement("events")]
    public List<Event> Events { get; set; } = new List<Event>();

    [BsonElement("actions")]
    public List<Action> Actions { get; set; } = new List<Action>();

    public Case Normalize(INormalizer n) => new Case
    {
        PublisherId = n.String(PublisherId),
        SystemId = n.String(SystemId),
        CaseId = n.String(CaseId),
        SubjectId = n.String(SubjectId),
        UpdateTime = n.Date(UpdateTime),
        Expires = n.Date(Expires),
        Organization = n.String(Organization),
        Label = n.String(Label),
        Description = n.String(Description),
        Status = n.String(Status),
        StatusHint = n.Enum<StatusHint>(StatusHint),
        Events = n.List(Events),
        Actions = n.List(Actions),
        IsDeleted = IsDeleted
    };
}
