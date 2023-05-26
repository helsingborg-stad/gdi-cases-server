namespace gdi_cases_server.Modules.Cases.Models.Normalization;

public static class NormalizableExtensions
{
    public static T Normalize<T>(this T normalizable) where T : INormalizable<T>, new() => new Normalizer().Object<T, T>(normalizable);

    public static TNew Normalize<T, TNew>(this T normalizable) where T : INormalizable<T> where TNew: T, new() => new Normalizer().Object<T, TNew>(normalizable);
}

