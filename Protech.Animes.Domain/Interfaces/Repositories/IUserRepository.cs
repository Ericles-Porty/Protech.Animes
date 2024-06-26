using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<bool> CreateAsync(User user);
    public Task<User?> GetByEmailAsync(string email);
}
