using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Domain.Interfaces.Services;

public interface IUserService
{
    Task<bool> Register(User user);
    Task<User?> GetByEmail(string email);
}