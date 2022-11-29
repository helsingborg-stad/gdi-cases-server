using System.ComponentModel;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;

namespace gdi_cases_server.Modules.Cases.Models;

public enum StatusHint
{
    [Description("The case should be displayed as approved and open")]
    Approved,
    [Description("The case should be displayed as rejected and open")]
    Rejected,
    [Description("The case should be displayed as closed")]
    Closed = 3,
    [Description("The case needs further refinement from the subject/applicant")]
    Incomplete = 4
}

