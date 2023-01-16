using System.ComponentModel;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;

namespace gdi_cases_server.Modules.Cases.Models;

public enum StatusHint
{
    [Description("The case should be displayed as approved and open")]
    approved,
    [Description("The case should be displayed as rejected and open")]
    rejected,
    [Description("The case should be displayed as closed")]
    closed = 3,
    [Description("The case needs further refinement from the subject/applicant")]
    incomplete = 4
}

