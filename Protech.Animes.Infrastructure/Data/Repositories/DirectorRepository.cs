using Microsoft.EntityFrameworkCore;
using Protech.Animes.Infrastructure.Data.Contexts;
using Protech.Animes.Infrastructure.Data.Repositories.Interfaces;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Infrastructure.Data.Repositories;

public class DirectorRepository : IDirectorRepository
{

    private readonly ProtechAnimesDbContext _dbContext;

    public DirectorRepository(ProtechAnimesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Director> CreateAsync(Director entity)
    {
        var director = await _dbContext.Directors.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return director.Entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var director = await _dbContext.Directors.FindAsync(id);

        if (director is null) return false;

        _dbContext.Directors.Remove(director);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Director>> GetAllAsync()
    {
        return await _dbContext.Directors
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Director?> GetByIdAsync(int id)
    {
        return await _dbContext.Directors.FindAsync(id);
    }


    public async Task<Director?> UpdateAsync(int id, Director entity)
    {
        var director = await GetByIdAsync(id);

        if (director is null) return null;

        director.Name = entity.Name;

        await _dbContext.SaveChangesAsync();

        return director;
    }

    public async Task<Director?> GetByNameAsync(string name)
    {
        return await _dbContext.Directors.FirstOrDefaultAsync(d => d.Name.Contains(name));
    }

    public async Task<IEnumerable<Director>> GetAllPaginatedAsync(int page, int pageSize)
    {
        return await _dbContext.Directors
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Director>> GetByNamePatternAsync(string name)
    {
        return await FilterDirectorsByNamePattern(name).ToListAsync();
    }

    public async Task<IEnumerable<Director>> GetByNamePatternPaginatedAsync(string name, int page, int pageSize)
    {
        return await FilterDirectorsByNamePattern(name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    private IQueryable<Director> FilterDirectorsByNamePattern(string name)
    {
        return _dbContext.Directors
            .AsNoTracking()
            .Where(d => EF.Functions.Like(d.Name, $"%{name}%"));
    }
}