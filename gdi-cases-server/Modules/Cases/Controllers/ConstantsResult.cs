using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace gdi_cases_server.Modules.Cases.Controllers
{
    //[DataContract(Name = "Constants")]
    public class ConstantsResult
    {
        //[DataMember()]
        [XmlElement(ElementName = "apa")]
        public IEnumerable<string>? StatusHints { get; internal set; }
        //[DataMember]
        public IEnumerable<string>? TypeHints { get; internal set; }
    }
}