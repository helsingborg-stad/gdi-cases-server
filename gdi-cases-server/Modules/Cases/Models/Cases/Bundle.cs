using System.Runtime.Serialization;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Cases;

public class Bundle: INormalizable<Bundle>
{
    [BsonElement("cases")]
    public List<Case> Cases { get; set; } = new List<Case>();

    public TNew Normalize<TNew>(INormalizer n) where TNew : Bundle, new() => new TNew
    {
        Cases = n.List<Case, Case>(Cases)
    };
}
