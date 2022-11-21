using System.ComponentModel;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;

namespace gdi_cases_server.Modules.Cases.Models;

public enum StatusHint
{
    [Description("The case should be displayed as approved (closed with some outcome or action")]
    Approved,
    [Description("The case should be displayed as rejected (closed without outcome or action)")]
    Rejected,
    [Description("The case should be displayed as closed")]
    Closed,
    [Description("The case needs further refinement from the subject/applicant")]
    Incomplete
}

