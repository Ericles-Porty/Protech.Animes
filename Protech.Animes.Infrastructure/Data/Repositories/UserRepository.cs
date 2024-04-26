
using Microsoft.EntityFrameworkCore;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Infrastructure.Data.Contexts;
using Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

namespace Protech.Animes.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{

    private readonly ProtechAnimesDbContext _context;

    public UserRepository(ProtechAnimesDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        var userCreated = await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return userCreated.Entity;
    }


    public async Task<User?> GetAsync(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);

        return user;
    }

}