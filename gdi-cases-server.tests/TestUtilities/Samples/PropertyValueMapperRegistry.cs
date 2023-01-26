using System.Reflection;

namespace gdi_cases_server.tests.TestUtilities.Samples;

public class PropertyValueMapperRegistry : ISampleInstanceProviderMappingRegistry
{
    public Dictionary<Type, PropertyValueMappingFunc> Mappings { get; }

    public PropertyValueMapperRegistry()
    {
        Mappings = new Dictionary<Type, PropertyValueMappingFunc>();
    }
    public ISampleInstanceProvider Build()
    {
        return new PropertyValueMapper(Mappings.ToDictionary(kv => kv.Key, kv => kv.Value));
    }

    public ISampleInstanceProviderMappingRegistry Map<T>(PropertyValueMappingFunc<T> mapper)
    {
        Mappings[typeof(T)] = (object value, PropertyInfo pi, ISampleInstanceProvider provider) => mapper((T)value, pi, provider);
        return this;
    }
}

