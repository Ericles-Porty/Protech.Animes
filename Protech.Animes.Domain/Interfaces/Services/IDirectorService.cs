using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Domain.Interfaces.Services;

public interface IDirectorService
{
    Task<IEnumerable<Director>> GetDirectors();
    Task<IEnumerable<Director>> GetDirectorsPaginated(int page, int pageSize);

    Task<Director> GetDirector(int id);

    Task<Director?> GetDirectorByName(string name);

    Task<IEnumerable<Director>> GetDirectorsByNamePattern(string name);
    Task<IEnumerable<Director>> GetDirectorsByNamePatternPaginated(string name, int page, int pageSize);

    Task<Director> CreateDirector(Director createDirector);

    Task<Director> UpdateDirector(int id, Director Director);

    Task<bool> DeleteDirector(int id);
}