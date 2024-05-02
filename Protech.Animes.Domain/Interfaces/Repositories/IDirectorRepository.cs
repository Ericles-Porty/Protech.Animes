using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Domain.Interfaces.Repositories;

public interface IDirectorRepository : IBaseRepository<Director, int>
{
    Task<Director?> GetByNameAsync(string name);

    Task<IEnumerable<Director>> GetAllPaginatedAsync(int page, int pageSize);

    Task<IEnumerable<Director>> GetByNamePatternAsync(string name);
    Task<IEnumerable<Director>> GetByNamePatternPaginatedAsync(string name, int page, int pageSize);
}