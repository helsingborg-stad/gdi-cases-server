using gdi_cases_server.Modules.Cases.Models.Normalization;

namespace gdi_cases_server.tests.Models.Normalization;

public class NormalizationTestBase : ModelTestBase
{
    public void AssertNormalize<T>(T source, T expectedNormalization) where T : class, INormalizable<T> => Assert.AreEqual(
        ToXml(expectedNormalization),
        ToXml(new Normalizer().Object(source)));
}

