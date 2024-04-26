using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.UseCases.DirectorUseCases;

public class DeleteDirectorUseCase
{
    private readonly IDirectorService _directorService;
    private readonly IAnimeService _animeService;

    public DeleteDirectorUseCase(IDirectorService directorService, IAnimeService animeService)
    {
        _directorService = directorService;
        _animeService = animeService;
    }

    public async Task<bool> Execute(int id)
    {
        var directorAnimes = await _animeService.GetAnimesByDirector(id);
        if (directorAnimes.Any()) throw new InvalidOperationException("Director has animes associated with it. Cannot delete.");

        return await _directorService.DeleteDirector(id);
    }
}