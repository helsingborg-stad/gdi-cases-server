using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace gdi_cases_server.XmlSupport
{
    /// <summary>
    /// Model Properties in XML are case sensitve, and for whatever reason Swashbuckle does not use the
    /// Name attribute of the DataContractAttribute. This filter looks for Models properties (all of
    /// of which are decorated with a DataContractAttribute) that have a Name value supplied,
    /// then replaces the corrosponding schema property that is properly cased.
    /// Inspired by thread at https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/989
    /// </summary>
    public class DescribeXmlProperties : ISchemaFilter
    {
        private static string? Coalesce(params string?[] candidates) => candidates.First(s => !string.IsNullOrEmpty(s));

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.GetCustomAttribute<DataContractAttribute>() != null)
            {
                (
                    from prop in context.Type.GetProperties()
                    let schemaProp = schema.Properties.FirstOrDefault(p => string.Equals(p.Key, prop.Name, StringComparison.OrdinalIgnoreCase))
                    where schemaProp.Key != null
                    let xmlName = Coalesce(prop.GetCustomAttribute<DataMemberAttribute>()?.Name, prop.Name)
                    where !string.IsNullOrEmpty(xmlName)
                    select new
                    {
                        schemaProp,
                        xmlName
                    })
                .ToList()
                .ForEach(rec =>
                {
                    if (rec.schemaProp.Value.Xml == null)
                    {
                        rec.schemaProp.Value.Xml = new OpenApiXml();
                    }
                    rec.schemaProp.Value.Xml.Name = rec.xmlName;
                });
            }
        }
    }
}
