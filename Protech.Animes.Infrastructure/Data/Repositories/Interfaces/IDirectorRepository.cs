namespace Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

using Protech.Animes.Domain.Entities;

public interface IDirectorRepository : IBaseRepository<Director, int>
{
    Task<Director?> GetByNameAsync(string name);

    Task<IEnumerable<Director>> GetAllPaginatedAsync(int page, int pageSize);

    Task<IEnumerable<Director>> GetByNamePatternAsync(string name);
    Task<IEnumerable<Director>> GetByNamePatternPaginatedAsync(string name, int page, int pageSize);
}