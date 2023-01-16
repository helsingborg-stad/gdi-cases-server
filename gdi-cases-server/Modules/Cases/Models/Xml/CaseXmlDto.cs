using gdi_cases_server.Converters;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace gdi_cases_server.Modules.Cases.Models.Xml;

[Description("A case")]
[DataContract(Name = "Case")]
public class CaseXmlDto
{
    [Required, StringLength(maximumLength: 128, MinimumLength = 3), Description("Publisher Id. Usually an organisation or self governed unit.")]
    [DataMember]
    public string PublisherId { get; set; } = "";

    [Required, StringLength(maximumLength: 128, MinimumLength = 3), Description("Distingushing Id of internal source of events within publisher")]
    [DataMember]
    public string SystemId { get; set; } = "";

    [Required, StringLength(maximumLength: 128, MinimumLength = 3), Description("System specific Id of event")]
    [DataMember]
    public string CaseId { get; set; } = "";

    [Required, Description("Subject of event. Usually a person")]
    [DataMember]
    public string SubjectId { get; set; } = "";

    [Required, Description("Time of update"), Range(typeof(DateTime), "1971-01-01", "3000-01-01")]
    [DataMember]
    public string UpdateTime { get; set; } = "";

    [Description("Display label")]
    [DataMember]
    public string Label { get; set; } = "";

    [Description("Display description")]
    [DataMember]
    public string Description { get; set; } = "";

    [Description("Display status")]
    [DataMember]
    public string? Status { get; set; } = "";

    [Description("Status hint")]
    [DataMember]
    public string? StatusHint { get; set; } = "";

    [DataMember]
    public List<CaseEventXmlDto>? Events { get; set; } = new List<CaseEventXmlDto>();

    [DataMember]
    public List<CaseActionXmlDto>? Actions { get; set; } = new List<CaseActionXmlDto>();

    [Description("Deleted flag")]
    [DataMember]
    public bool IsDeleted { get; set; } = false;
}

