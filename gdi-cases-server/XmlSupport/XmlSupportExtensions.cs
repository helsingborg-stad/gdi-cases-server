using System;
using System.Reflection;
using System.Runtime.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace gdi_cases_server.XmlSupport
{
    public static class XmlSupport
    {
        private static string? Coalesce(params string?[] candidates) => candidates.First(s => !string.IsNullOrEmpty(s));

        public static IServiceCollection AddCasesXmlSupport(this IServiceCollection services) => services.AddSwaggerGen(ConfigureSwaggerGen);
        
        private static void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            options.SchemaFilter<DescribeXmlCollections>();
            options.SchemaFilter<DescribeXmlProperties>();

            // DataContractAttribute.Name is not honored for some reason, so we have to override it
            options.CustomSchemaIds(type => Coalesce(type.GetCustomAttribute<DataContractAttribute>()?.Name, type.Name));
        }
    }
}
