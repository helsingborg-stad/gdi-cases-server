﻿namespace gdi_cases_server.Authentication;

public class CasesApiKeys : ICasesApiKeys
{
    public CasesApiKeys(Func<string, bool> validateKey)
    {
        ValidateKey = validateKey;
    }

    public Func<string, bool> ValidateKey { get; }

    public static ICasesApiKeys? TryCreateFromEnv()
    {
        var apiKey = (Environment.GetEnvironmentVariable("API_KEY") ?? "").Trim();

        return string.IsNullOrEmpty(apiKey) ? null : new CasesApiKeys(key => string.Equals(key, apiKey));
    }

    public bool IsValidApiKey(string key) => ValidateKey(key);
}