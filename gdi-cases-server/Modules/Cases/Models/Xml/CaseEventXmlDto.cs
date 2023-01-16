using gdi_cases_server.Converters;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace gdi_cases_server.Modules.Cases.Models.Xml;

[Description("A description of a milestone in a case process")]
[DataContract(Name = "Event")]
public class CaseEventXmlDto
{
    [Required]
    [DataMember(Name = "UpdateTime")]
    public string UpdateTime { get; set; } = "";

    [Description("Display label")]
    [DataMember(Name = "Label")]
    public string Label { get; set; } = "";

    [Description("Display description")]
    [DataMember(Name = "Description")]
    public string Description { get; set; } = "";

    [Description("Display status")]
    [DataMember(Name = "Status")]
    public string? Status { get; set; } = "";

    [Description("Status hint")]
    [DataMember(Name = "StatusHint")]
    public string? StatusHint { get; set; } = "";

    [DataMember(Name = "Actions")]
    public List<CaseActionXmlDto>? Actions { get; set; } = new List<CaseActionXmlDto>();
}
