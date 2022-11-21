using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using gdi_cases_server.Converters;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models;

[Description("A description of a milestone in a case process")]
public class CaseEvent
{
    [Required, BsonElement("updateTime")]
    public string UpdateTime { get; set; } = "";

    [BsonElement("label"), Description("Display label")]
    public string Label { get; set; } = "";

    [BsonElement("description"), Description("Display description")]
    public string Description { get; set; } = "";

    [BsonElement("status"), Description("Display status")]
    public string? Status { get; set; } = "";

    /*
    [BsonElement("statusHint"), Description("Status hint")]
    public StatusHint StatusHint { get; set; } = StatusHint.Unknown;
    */
    [BsonElement("statusHint"), Description("Status hint")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<StatusHint>))]
    public string? StatusHint { get; set; } = "";

    [BsonElement("actions")]
    public IEnumerable<CaseAction>? Actions { get; set; } = Enumerable.Empty<CaseAction>();
}

