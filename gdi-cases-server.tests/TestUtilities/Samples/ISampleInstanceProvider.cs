namespace gdi_cases_server.tests.TestUtilities.Samples;

public interface ISampleInstanceProvider
{
    T Map<T>() where T : class, new();
}

