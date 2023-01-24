using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace gdi_cases_server.Modules.Cases.Controllers
{
    public class ConstantsResult
    {
        public List<string> StatusHints { get; internal set; } = new List<string>();
        public List<string>? TypeHints { get; internal set; } = new List<string>();
    }
}