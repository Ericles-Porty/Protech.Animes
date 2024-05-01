namespace Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

using Protech.Animes.Domain.Entities;

public interface IAnimeRepository : IBaseRepository<Anime, int>
{
    Task<Anime?> GetByNameAsync(string name);

    Task<Anime?> CreateAnimeWithDirectorAsync(Anime anime, Director director);

    Task<Anime?> UpdateAnimeWithNewDirectorAsync(Anime anime, Director director);

    Task<Anime?> GetByIdIncludingDirectorAsync(int id);

    Task<IEnumerable<Anime>> GetAllPaginatedAsync(int page, int pageSize);

    Task<IEnumerable<Anime>> GetByNamePatternAsync(string name);
    Task<IEnumerable<Anime>> GetByNamePatternPaginatedAsync(string name, int page, int pageSize);

    Task<IEnumerable<Anime>> GetByDirectorIdAsync(int directorId);
    Task<IEnumerable<Anime>> GetByDirectorIdPaginatedAsync(int directorId, int page, int pageSize);

    Task<IEnumerable<Anime>> GetByDirectorNameAsync(string directorName);
    Task<IEnumerable<Anime>> GetByDirectorNamePaginatedAsync(string directorName, int page, int pageSize);

    Task<IEnumerable<Anime>> GetByKeywordSummaryAsync(string keyword);
    Task<IEnumerable<Anime>> GetByKeywordSummaryPaginatedAsync(string keyword, int page, int pageSize);
}