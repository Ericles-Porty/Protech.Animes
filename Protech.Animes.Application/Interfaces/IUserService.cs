using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Application.Interfaces;

public interface IUserService
{
    Task<User> Register(User user);

    Task<User> Login(string email, string password);

}