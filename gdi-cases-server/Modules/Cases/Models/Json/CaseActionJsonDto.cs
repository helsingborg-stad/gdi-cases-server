using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using gdi_cases_server.Converters;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace gdi_cases_server.Modules.Cases.Models.Json;

[Description("Actions/links to external processes")]
public class CaseActionJsonDto
{
    [Required, BsonElement("label"), Description("Display label for action")]
    public string Label { get; set; } = "";

    [Required, BsonElement("url"), Url, Description("Link to e-service")]
    public string Url { get; set; } = "";

    [BsonElement("typeHint"), Description("Type of action")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<ActionTypeHint>))]
    public string? TypeHint { get; set; } = "";
}
