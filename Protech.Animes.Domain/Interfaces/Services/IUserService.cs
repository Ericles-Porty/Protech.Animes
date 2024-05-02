using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Domain.Interfaces.Services;

public interface IUserService
{
    Task<User> Register(User user);
    Task<User?> GetByEmail(string email);
}