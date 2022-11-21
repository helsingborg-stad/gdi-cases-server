using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using gdi_cases_server.Converters;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models;

[Description("Actions/links to external processes")]
public class CaseAction
{
    [Required, BsonElement("label"), Description("Display label for action")]
    public string Label { get; set; } = "";

    [Required, BsonElement("url"), Url, Description("Link to e-service")]
    public string Url { get; set; } = "";

    /*
    [BsonElement("typeHint"), Description("Type of action")]
    [JsonConverter(typeof(RelaxedEnumConverter<ActionTypeHint>))]
    public ActionTypeHint TypeHint { get; set; } = ActionTypeHint.Unknown;
    */
    [BsonElement("typeHint"), Description("Type of action")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<ActionTypeHint>))]
    public string? TypeHint { get; set; } = "";
}

