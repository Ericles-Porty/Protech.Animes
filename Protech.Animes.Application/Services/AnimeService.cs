using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Repositories;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.Services;

public class AnimeService : IAnimeService
{

    private readonly IAnimeRepository _animeRepository;

    public AnimeService(IAnimeRepository animeRepository)
    {
        _animeRepository = animeRepository;
    }

    public async Task<Anime> CreateAnime(Anime anime)
    {
        return await _animeRepository.CreateAsync(anime);
    }

    public async Task<bool> DeleteAnime(int id)
    {
        return await _animeRepository.DeleteAsync(id);
    }

    public async Task<Anime> GetAnime(int id)
    {
        var anime = await _animeRepository.GetByIdIncludingDirectorAsync(id);
        if (anime is null) throw new NotFoundException("Anime not found");

        return anime;
    }

    public async Task<Anime?> GetAnimeByName(string name)
    {
        return await _animeRepository.GetByNameAsync(name);
    }

    public async Task<IEnumerable<Anime>> GetAnimesByNamePattern(string name)
    {
        return await _animeRepository.GetByNamePatternAsync(name);
    }

    public async Task<IEnumerable<Anime>> GetAnimesByNamePatternPaginated(string name, int page, int pageSize)
    {
        return await _animeRepository.GetByNamePatternPaginatedAsync(name, page, pageSize);
    }

    public async Task<IEnumerable<Anime>> GetAnimes()
    {
        return await _animeRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Anime>> GetAnimesByDirector(int directorId)
    {
        return await _animeRepository.GetByDirectorIdAsync(directorId);
    }

    public async Task<IEnumerable<Anime>> GetAnimesByDirectorPaginated(int directorId, int page, int pageSize)
    {
        return await _animeRepository.GetByDirectorIdPaginatedAsync(directorId, page, pageSize);
    }

    public async Task<IEnumerable<Anime>> GetAnimesByDirectorName(string directorName)
    {
        return await _animeRepository.GetByDirectorNameAsync(directorName);
    }

    public async Task<IEnumerable<Anime>> GetAnimesByDirectorNamePaginated(string directorName, int page, int pageSize)
    {
        return await _animeRepository.GetByDirectorNamePaginatedAsync(directorName, page, pageSize);
    }

    public async Task<IEnumerable<Anime>> GetAnimesBySummaryKeyword(string keyword)
    {
        return await _animeRepository.GetByKeywordSummaryAsync(keyword);
    }

    public async Task<IEnumerable<Anime>> GetAnimesBySummaryKeywordPaginated(string keyword, int page, int pageSize)
    {
        return await _animeRepository.GetByKeywordSummaryPaginatedAsync(keyword, page, pageSize);
    }

    public async Task<IEnumerable<Anime>> GetAnimesPaginated(int page, int pageSize)
    {
        return await _animeRepository.GetAllPaginatedAsync(page, pageSize);
    }

    public async Task<Anime> UpdateAnime(Anime anime)
    {
        var updatedAnime = await _animeRepository.UpdateAsync(anime);
        if (updatedAnime is null) throw new NotFoundException("Anime not found");

        return updatedAnime;
    }

}