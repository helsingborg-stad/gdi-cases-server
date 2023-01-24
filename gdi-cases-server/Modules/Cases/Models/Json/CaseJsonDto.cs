using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using gdi_cases_server.Converters;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Json;

[Description("A case")]
public class Case
{
    [Required, BsonElement("publisherId"), StringLength(maximumLength: 128, MinimumLength = 3), Description("Publisher Id. Usually an organisation or self governed unit.")]
    public string PublisherId { get; set; } = "";

    [Required, BsonElement("systemId"), StringLength(maximumLength: 128, MinimumLength = 3), Description("Distingushing Id of internal source of events within publisher")]
    public string SystemId { get; set; } = "";

    [Required, BsonElement("caseId"), StringLength(maximumLength: 128, MinimumLength = 8), Description("System specific Id of event")]
    public string CaseId { get; set; } = "";

    [Required, BsonElement("subjectId"), Description("Subject of event. Usually a person")]
    public string SubjectId { get; set; } = "";

    [Required, BsonElement("updateTime"), Description("Time of update"), Range(typeof(DateTime), "1971-01-01", "3000-01-01")]
    public string UpdateTime { get; set; } = "";

    [BsonElement("label"), Description("Display label")]
    public string Label { get; set; } = "";

    [BsonElement("description"), Description("Display description")]
    public string Description { get; set; } = "";

    [BsonElement("status"), Description("Display status")]
    public string? Status { get; set; } = "";

    [BsonElement("statusHint"), Description("Status hint")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<StatusHint>))]
    public string? StatusHint { get; set; } = "";

    [BsonElement("events")]
    public List<Event>? Events { get; set; } = new List<Event>();

    [BsonElement("actions")]
    public List<Action>? Actions { get; set; } = new List<Action>();

    [BsonIgnore, Description("Deleted flag")]
    public bool IsDeleted { get; set; } = false;
}
