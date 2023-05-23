namespace gdi_cases_server.Modules.Cases.Models.Normalization;

public class Normalizer : INormalizer
{
    public class EnumNormalizer<T> where T : struct, Enum
    {
        static readonly Dictionary<string, string> Mapping = System.Enum.GetNames<T>().ToDictionary(name => name, name => name, StringComparer.OrdinalIgnoreCase);

        public static string Normalize(string value) {
            var mapped = string.Empty;
            return Mapping.TryGetValue(value, out mapped) ? mapped : string.Empty;
        }
    }

    public string Date(string value) => String(value);

    public string Enum<T>(string value) where T : struct, Enum => EnumNormalizer<T>.Normalize(value);

    public List<TNew> List<T, TNew>(List<T> list) where T : INormalizable<T> where TNew : T, new() => (list ?? Enumerable.Empty<T>()).Where(item => item != null).Select(item => item.Normalize<TNew>(this)).ToList();

    public TNew Object<T, TNew>(T value) where T : INormalizable<T> where TNew : T, new() => value.Normalize<TNew>(this);

    public string String(string value) => string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
}

