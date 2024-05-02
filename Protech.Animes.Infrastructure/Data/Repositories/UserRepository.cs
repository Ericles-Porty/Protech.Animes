
using Microsoft.EntityFrameworkCore;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Interfaces.Repositories;
using Protech.Animes.Infrastructure.Data.Contexts;

namespace Protech.Animes.Infrastructure.Data.Repositories;

public class UserRepository(ProtechAnimesDbContext context) : IUserRepository
{
    private readonly ProtechAnimesDbContext _context = context;

    public async Task<User> CreateAsync(User user)
    {
        var userCreated = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return userCreated.Entity;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}