using System.Text.Json;

namespace gdi_cases_server.Modules.Cases.Models.Normalization;

public interface INormalizer
{
    TNew Object<T, TNew>(T value) where T : INormalizable<T> where TNew: T, new();
    string String(string value);
    string Date(string value);
    string Enum<T>(string value) where T : struct, Enum;
    List<TNew> List<T, TNew>(List<T> list) where T: INormalizable<T> where TNew: T, new();
}
