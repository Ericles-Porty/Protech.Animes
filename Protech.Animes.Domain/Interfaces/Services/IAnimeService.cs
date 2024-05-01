using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Domain.Interfaces.Services;

public interface IAnimeService
{
    Task<IEnumerable<Anime>> GetAnimes();
    Task<IEnumerable<Anime>> GetAnimesPaginated(int page, int pageSize);

    Task<Anime> GetAnime(int id);
    Task<Anime?> GetAnimeByName(string name);

    Task<Anime> CreateAnime(Anime anime);

    Task<Anime> UpdateAnime(int id, Anime anime);

    Task<bool> DeleteAnime(int id);

    Task<IEnumerable<Anime>> GetAnimesByNamePattern(string name);
    Task<IEnumerable<Anime>> GetAnimesByNamePatternPaginated(string name, int page, int pageSize);

    Task<IEnumerable<Anime>> GetAnimesByDirector(int directorId);
    Task<IEnumerable<Anime>> GetAnimesByDirectorPaginated(int directorId, int page, int pageSize);

    Task<IEnumerable<Anime>> GetAnimesByDirectorName(string directorName);
    Task<IEnumerable<Anime>> GetAnimesByDirectorNamePaginated(string directorName, int page, int pageSize);

    Task<IEnumerable<Anime>> GetAnimesBySummaryKeyword(string keyword);
    Task<IEnumerable<Anime>> GetAnimesBySummaryKeywordPaginated(string keyword, int page, int pageSize);
}