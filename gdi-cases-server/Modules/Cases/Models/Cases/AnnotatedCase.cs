using System.ComponentModel;

namespace gdi_cases_server.Modules.Cases.Models.Cases;

[Description("An annotated case")]
public class AnnotatedCase: Case
{
    [Description("Composite Id")]
    public string RecordId { get; set; } = "";
    [Description("Flag for case read by end user")]
    public bool IsMarkedAsRead { get; set; }
}
