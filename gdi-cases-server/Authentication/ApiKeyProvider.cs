using System.Security.Claims;
using AspNetCore.Authentication.ApiKey;

namespace gdi_cases_server.Authentication
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        public ApiKeyProvider(ICasesApiKeys apiKeys)
        {
            ApiKeys = apiKeys;
        }

        public ICasesApiKeys ApiKeys { get; }

        public async Task<IApiKey> ProvideAsync(string key)
        {
            return ApiKeys.IsValidApiKey(key) ? new ApiKey(key) : null;
        }

        public class ApiKey : IApiKey
        {
            public ApiKey(string key, string owner = "cases api", IReadOnlyCollection<Claim>? claims = null)
            {
                Key = key;
                OwnerName = owner;
                Claims = claims ?? new List<Claim>();
            }

            public string Key { get; }

            public string OwnerName { get; }

            public IReadOnlyCollection<Claim> Claims { get; }
        }
    }
}

