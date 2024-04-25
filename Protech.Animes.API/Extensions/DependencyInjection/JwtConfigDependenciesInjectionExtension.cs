using Protech.Animes.Application.Configurations;

namespace Protech.Animes.API.Extensions.DependencyInjection;

public static class JwtConfigDependenciesInjectionExtension
{
    public static IServiceCollection AddJwtConfigDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

        return services;
    }
}