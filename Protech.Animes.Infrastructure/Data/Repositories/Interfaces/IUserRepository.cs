using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateAsync(User user);
        public Task<User?> GetByEmailAsync(string email);
    }
}