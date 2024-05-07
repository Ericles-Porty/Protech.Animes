using Microsoft.AspNetCore.Identity;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Interfaces.Repositories;

namespace Protech.Animes.Infrastructure.Data.Repositories;

public class UserRepository(UserManager<IdentityUser> userManager) : IUserRepository
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public async Task<bool> CreateAsync(User user)
    {
        var identityUser = new IdentityUser
        {
            UserName = user.UserName,
            NormalizedUserName = user.UserName.ToUpper(),
            Email = user.Email,
            NormalizedEmail = user.Email.ToUpper(),
        };

        var result = await _userManager.CreateAsync(identityUser, user.Password);
        return result.Succeeded;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return null;

        var userEntity = new User
        {
            Id = user.Id,
            UserName = user.UserName ?? throw new NullReferenceException("User name is null"),
            Email = user.Email ?? throw new NullReferenceException("User email is null"),
            Password = user.PasswordHash ?? throw new NullReferenceException("User password is null")
        };
        return userEntity;
    }

}