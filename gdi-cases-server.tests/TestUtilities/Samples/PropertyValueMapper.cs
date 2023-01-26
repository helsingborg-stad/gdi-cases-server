using System;
using System.Reflection;

namespace gdi_cases_server.tests.TestUtilities.Samples;

public delegate object PropertyValueMappingFunc(object value, PropertyInfo propertyInfo, ISampleInstanceProvider provider);
public delegate T PropertyValueMappingFunc<T>(T value, PropertyInfo propertyInfo, ISampleInstanceProvider provider);

public class PropertyValueMapper : ISampleInstanceProvider
{
    public PropertyValueMapper(IDictionary<Type, PropertyValueMappingFunc> mappings)
    {
        Mappings = mappings;
    }

    public IDictionary<Type, PropertyValueMappingFunc> Mappings { get; }

    public T Map<T>() where T : class, new()
    {
        var instance = new T();
        (from pi in typeof(T).GetRuntimeProperties()
         let getter = pi.GetGetMethod()
         where getter != null
         let setter = pi.GetSetMethod()
         where setter != null
         let mapper = GetMapper(pi.PropertyType)
         where mapper != null
         let v = Apply(instance, pi, mapper)
         select v).ToList();
        return instance;
    }

    private object Apply<T>(T instance, PropertyInfo pi, PropertyValueMappingFunc mapper)
    {
        var initial = pi.GetValue(instance);
        var mapped = mapper(initial, pi, this);
        pi.SetValue(
            instance,
            mapped);
        return mapped;
    }

    protected PropertyValueMappingFunc GetMapper(Type type)
    {
        PropertyValueMappingFunc mapper = Unmapped;
        return Mappings.TryGetValue(type, out mapper) ? mapper : Unmapped;
    }

    protected object Unmapped(object value, PropertyInfo pi, ISampleInstanceProvider provider)
    {
        throw new ApplicationException($"No mapping exists for property {pi.Name} in class {pi.DeclaringType?.Name}");
    }
}

