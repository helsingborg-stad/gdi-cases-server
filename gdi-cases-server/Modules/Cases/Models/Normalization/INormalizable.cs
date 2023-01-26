using System;
namespace gdi_cases_server.Modules.Cases.Models.Normalization;

public interface INormalizable<T>
{
    T Normalize(INormalizer n);
}

