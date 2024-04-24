namespace Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

using Protech.Animes.Domain.Entities;

public interface IDirectorRepository : IBaseRepository<Director>
{
    Task<Director?> GetByNameAsync(string name);
}