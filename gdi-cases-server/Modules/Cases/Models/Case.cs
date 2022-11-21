using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using gdi_cases_server.Converters;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models;

[Description("A case")]
public class Case
{
    [Required, BsonElement("caseId"), StringLength(maximumLength: 24, MinimumLength = 8), Description("System specific Id of event")]
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

    /*
    [BsonElement("statusHint"), Description("Status hint")]
    [JsonConverter(typeof(RelaxedEnumConverter<StatusHint>))]
    public StatusHint StatusHint { get; set; } = StatusHint.Unknown;
    */
    [BsonElement("statusHint"), Description("Status hint")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<StatusHint>))]
    public string? StatusHint { get; set; } = "";

    [BsonElement("events")]
    public IEnumerable<CaseEvent>? Events { get; set; } = Enumerable.Empty<CaseEvent>();

    [BsonElement("actions")]
    public IEnumerable<CaseAction>? Actions { get; set; } = Enumerable.Empty<CaseAction>();
}

