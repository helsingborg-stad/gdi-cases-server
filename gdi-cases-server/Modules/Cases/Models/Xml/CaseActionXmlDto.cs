using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace gdi_cases_server.Modules.Cases.Models.Xml;

[Description("Actions/links to external processes")]
//[DataContract(Name = "Action")]
public class CaseActionXmlDto
{
    [Required, Description("Display label for action")]
    //[DataMember]
    public string Label { get; set; } = "";

    [Required, Url, Description("Link to e-service")]
    //[DataMember]
    public string Url { get; set; } = "";

    [Description("Type of action")]
    //[DataMember]
    public string? TypeHint { get; set; } = "";
}


