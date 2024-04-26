using Protech.Animes.Application.UseCases.AnimeUseCases;
using Protech.Animes.Application.UseCases.AuthUseCases;
using Protech.Animes.Application.UseCases.DirectorUseCases;

namespace Protech.Animes.API.Extensions.DependencyInjection;

public static class UseCasesDependenciesInjectionExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        #region Auth
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUserUseCase>();
        #endregion

        #region Anime
        services.AddScoped<CreateAnimeUseCase>();
        services.AddScoped<UpdateAnimeUseCase>();
        services.AddScoped<GetAnimesUseCase>();
        services.AddScoped<GetAnimeUseCase>();
        services.AddScoped<DeleteAnimeUseCase>();
        services.AddScoped<GetAnimesByNameUseCase>();
        services.AddScoped<GetAnimesByDirectorUseCase>();
        services.AddScoped<GetAnimesByDirectorNameUseCase>();
        services.AddScoped<GetAnimesBySummaryKeywordUseCase>();
        #endregion

        #region Director
        services.AddScoped<CreateDirectorUseCase>();
        services.AddScoped<UpdateDirectorUseCase>();
        services.AddScoped<GetDirectorsUseCase>();
        services.AddScoped<GetDirectorUseCase>();
        services.AddScoped<DeleteDirectorUseCase>();
        services.AddScoped<GetDirectorsByNameUseCase>();
        #endregion

        return services;
    }
}