using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace gdi_cases_server.Modules.Cases.Models.Xml;

/// <summary>
/// This class breaks the naming convention
/// - by convention, the name should be BundleXmlDto
/// - but, the root element in Xml can not be set with DataContract nor XmlRoot
/// So far, the only way to actually force a structure like
///     <Bundle>...</Bundle>
/// is to derive the root eleent name directly from the class nane.
/// </summary>
// [DataContract(Name = "Bundle")] - DOES NOT DEFINE ROOT ELEMENT NAME
// [XmlRoot(ElementName = "Bundle")] - DOES NOT DEFINE ROOT ELEMENT NAME
[DataContract]
[XmlRoot]
public class Bundle
{
    [DataMember]
    public List<CaseXmlDto>? Cases { get; set; } = new List<CaseXmlDto>();
}

