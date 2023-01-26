using System.Text.Json;

namespace gdi_cases_server.Modules.Cases.Models.Normalization;

public interface INormalizer
{
    T Object<T>(T value) where T : INormalizable<T>;
    string String(string value);
    string Date(string value);
    string Enum<T>(string value) where T : struct, Enum;
    List<T> List<T>(List<T> list) where T: INormalizable<T>;
}
