namespace gdi_cases_server.Modules.Cases.Models.Normalization;

public static class NormalizableExtensions
{
    public static T Normalize<T>(this T normalizable) where T : INormalizable<T> => new Normalizer().Object(normalizable);
}

