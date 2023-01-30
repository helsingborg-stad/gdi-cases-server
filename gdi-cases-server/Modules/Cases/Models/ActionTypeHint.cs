using gdi_cases_server.Converters;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace gdi_cases_server.Modules.Cases.Models;

public enum ActionTypeHint
{
    [Description("The acction should be presented as a web link")]
    link,
    [Description("The action should be presented as a downloadable document")]
    document,
    [Description("The action should be presented as an image")]
    image
}

