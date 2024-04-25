using Protech.Animes.Application.UseCases;

namespace Protech.Animes.API.Extensions.DependencyInjection;

public static class UseCasesDependenciesInjectionExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateAnimeUseCase>();
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUserUseCase>();
        
        return services;
    }
}