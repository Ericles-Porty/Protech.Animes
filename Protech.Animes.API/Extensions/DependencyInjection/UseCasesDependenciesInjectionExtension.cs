using Protech.Animes.Application.UseCases.AnimeUseCases;
using Protech.Animes.Application.UseCases.AuthUseCases;

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
        services.AddScoped<GetAnimesBySummaryKeywordUseCase>();

        return services;
    }
}