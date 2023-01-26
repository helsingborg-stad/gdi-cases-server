namespace gdi_cases_server.tests.TestUtilities.Samples;

public interface ISampleInstanceProviderMappingRegistry
{
    ISampleInstanceProviderMappingRegistry Map<T>(PropertyValueMappingFunc<T> mapper);
    ISampleInstanceProvider Build();
}
