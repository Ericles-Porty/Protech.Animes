using Microsoft.AspNetCore.Identity;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Interfaces.Repositories;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Register(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }
}