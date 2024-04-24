namespace Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

using Protech.Animes.Domain.Entities;

public interface IAnimeRepository : IBaseRepository<Anime>
{
    public Task<Anime?> GetByIdIncludingDirectorAsync(int id);
    public Task<IEnumerable<Anime>> GetAllPaginatedAsync(int page, int pageSize);
    public Task<IEnumerable<Anime>> GetByDirectorIdAsync(int directorId);
    public Task<IEnumerable<Anime>> GetByDirectorIdPaginatedAsync(int directorId, int page, int pageSize);
    public Task<IEnumerable<Anime>> GetByKeywordSummaryAsync(string keyword);
    public Task<IEnumerable<Anime>> GetByKeywordSummaryPaginatedAsync(string keyword, int page, int pageSize);
}