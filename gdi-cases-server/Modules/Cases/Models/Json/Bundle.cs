using System.Runtime.Serialization;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using MongoDB.Bson.Serialization.Attributes;

namespace gdi_cases_server.Modules.Cases.Models.Json;

public class Bundle: INormalizable<Bundle>
{
    [BsonElement("cases")]
    public List<Case> Cases { get; set; } = new List<Case>();

    public Bundle Normalize(INormalizer n) => new Bundle
    {
        Cases = n.List(Cases)
    };
}
