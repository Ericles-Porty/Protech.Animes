using Protech.Animes.Application.UseCases.Anime;
using Protech.Animes.Application.UseCases.Auth;

namespace Protech.Animes.API.Extensions.DependencyInjection;

public static class UseCasesDependenciesInjectionExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateAnimeUseCase>();
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUserUseCase>();
        services.AddScoped<UpdateAnimeUseCase>();
        services.AddScoped<GetAnimesUseCase>();
        services.AddScoped<GetAnimeUseCase>();
        services.AddScoped<DeleteAnimeUseCase>();
        services.AddScoped<GetAnimesByNameUseCase>();
        services.AddScoped<GetAnimesByDirectorUseCase>();
        services.AddScoped<GetAnimesByDirectorNameUseCase>();

        return services;
    }
}