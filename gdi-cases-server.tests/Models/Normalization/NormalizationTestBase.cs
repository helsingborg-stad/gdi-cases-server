using gdi_cases_server.Modules.Cases.Models.Normalization;

namespace gdi_cases_server.tests.Models.Normalization;

public class NormalizationTestBase : ModelTestBase
{
    public void AssertNormalize<T>(T source, T expectedNormalization) where T : INormalizable<T>, new() => Assert.AreEqual(
        ToXml(expectedNormalization),
        ToXml(new Normalizer().Object<T, T>(source)));
}

