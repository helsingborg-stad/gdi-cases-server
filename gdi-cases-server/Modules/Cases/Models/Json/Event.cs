using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Json;

[Description("A description of a milestone in a case process")]
public class Event: INormalizable<Event>
{
    [Required, BsonElement("updateTime"), Range(typeof(DateTime), "1971-01-01", "3000-01-01")]
    public string UpdateTime { get; set; } = "";

    [BsonElement("label"), Description("Display label")]
    public string Label { get; set; } = "";

    [BsonElement("description"), Description("Display description")]
    public string Description { get; set; } = "";

    [BsonElement("status"), Description("Display status")]
    public string Status { get; set; } = "";

    [BsonElement("statusHint"), Description("Status hint")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<StatusHint>))]
    public string StatusHint { get; set; } = "";

    [BsonElement("actions")]
    public List<Action> Actions { get; set; } = new List<Action>();

    public Event Normalize(INormalizer n) => new Event
    {
        UpdateTime = n.Date(UpdateTime),
        Label = n.String(Label),
        Description = n.String(Description),
        Status = n.String(Status),
        StatusHint = n.Enum<StatusHint>(StatusHint),
        Actions = n.List<Action>(Actions)
    };
}

