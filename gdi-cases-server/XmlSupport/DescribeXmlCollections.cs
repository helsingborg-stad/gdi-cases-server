using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace gdi_cases_server.XmlSupport
{
    /// <summary>
    /// swagger XML representation doesn't include the initial array item element, which
    /// confuses the XMLDataContractSerializer. To fix this, we have to add an Xml schema
    /// to the array object, and change the ref of the item to be AllOf, which specificies
    /// that the object must conform to the schema provided for AllOf. We can then apply an
    /// XML schema with the name attribute provided. If we didn't change the ref to AllOf,
    /// the XML Name would be ignored.
    /// Inspired by thread at https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/989
    /// </summary>
    public class DescribeXmlCollections : ISchemaFilter
    {
        private static string? Coalesce(params string?[] candidates) => candidates.First(s => !string.IsNullOrEmpty(s));

        private Type? GetEnumeratedType(Type from)
        {
            if (from.IsGenericType && from.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return from.GetGenericArguments()[0];
            }

            return from.GetInterfaces()
                .Select(ifc => GetEnumeratedType(ifc))
                .FirstOrDefault(ifc => ifc != null)
                ?? from.GetElementType();
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Type == "array")
            {
                if (schema.Xml == null)
                {
                    schema.Xml = new OpenApiXml();
                }
                schema.Xml.Wrapped = true;

                if (schema.Items.Xml == null)
                {
                    schema.Items.Xml = new OpenApiXml();
                }
                schema.Items.Xml.Name = Coalesce(
                    context.Type.GetCustomAttribute<CollectionDataContractAttribute>()?.ItemName,
                    GetEnumeratedType(context.Type)?.GetCustomAttribute<DataContractAttribute>()?.Name,
                    GetEnumeratedType(context.Type)?.Name,
                    context.Type.Name
                );

                if (schema.Items?.Reference != null)
                {
                    schema.Items.Type = "object";
                    schema.Items.AllOf = new List<OpenApiSchema> { new OpenApiSchema { Reference = schema.Items.Reference } };
                    schema.Items.Reference = null;
                }
            }
        }
    }
}
