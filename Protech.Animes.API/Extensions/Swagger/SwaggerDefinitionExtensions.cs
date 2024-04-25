using Microsoft.OpenApi.Models;

namespace Protech.Animes.API.Extensions.Swagger;

public static class SwaggerDefinitionExtensions
{
    public static IServiceCollection AddSwaggerDefinition(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "Protech.Animes.API",
                Version = "v1",
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization : Bearer { token }\"",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                },
            });
        });

        return services;
    }
}