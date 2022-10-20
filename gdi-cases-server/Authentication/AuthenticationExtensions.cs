using AspNetCore.Authentication.ApiKey;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace gdi_cases_server.Authentication;


public static class CasesApiKey
{
    public static IServiceCollection AddCasesAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
            .AddApiKeyInAuthorizationHeader<ApiKeyProvider>(c =>
            {
                c.Realm = "Cases API";
                c.KeyName = "bearer";
            });

        services.AddSingleton<ICasesApiKeys>(CasesApiKeys.TryCreateFromEnv() ?? CasesApiKeys.ConfigurationError());
        return services;
    }

    public static void ConfigureSwaggerGen(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("apiKeyAuth", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "API Key",
            Description = "API Key authorization header using the Bearer scheme."
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "apiKeyAuth" }
                },
                new string[] {}
            }
        });
    }
}

