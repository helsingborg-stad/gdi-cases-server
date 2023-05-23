using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Cases;

[Description("Actions/links to external processes")]
public class Action: INormalizable<Action>
{
    [Required, BsonElement("label"), Description("Display label for action")]
    public string Label { get; set; } = "";

    [Required, BsonElement("url"), Url, Description("Link to e-service")]
    public string Url { get; set; } = "";

    [BsonElement("typeHint"), Description("Type of action")]
    [JsonConverter(typeof(StringValuesFromEnumConverter<ActionTypeHint>))]
    public string TypeHint { get; set; } = "";

    public TNew Normalize<TNew>(INormalizer n) where TNew : Action, new() => new TNew
    {
        Label = n.String(Label),
        Url = n.String(Url),
        TypeHint = n.Enum<ActionTypeHint>(TypeHint)
    };
}
