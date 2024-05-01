using Microsoft.EntityFrameworkCore;
using Protech.Animes.Infrastructure.Data.Contexts;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Interfaces.Repositories;

namespace Protech.Animes.Infrastructure.Data.Repositories;

public class DirectorRepository(ProtechAnimesDbContext dbContext) : BaseRepository<Director, int>(dbContext), IDirectorRepository
{
    private readonly ProtechAnimesDbContext _dbContext = dbContext;

    public async Task<Director?> GetByNameAsync(string name)
    {
        return await _dbContext.Directors.FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<Director>> GetAllPaginatedAsync(int page, int pageSize)
    {
        var query = _dbContext.Directors.AsNoTracking();
        return await Paginate(query, page, pageSize).ToListAsync();
    }

    public async Task<IEnumerable<Director>> GetByNamePatternAsync(string name)
    {
        return await FilterDirectorsByNamePattern(name).ToListAsync();
    }

    public async Task<IEnumerable<Director>> GetByNamePatternPaginatedAsync(string name, int page, int pageSize)
    {
        return await Paginate(FilterDirectorsByNamePattern(name), page, pageSize).ToListAsync();
    }

    private IQueryable<Director> FilterDirectorsByNamePattern(string name)
    {
        return _dbContext.Directors
            .AsNoTracking()
            .Where(d => EF.Functions.ILike(d.Name, $"%{name}%"));
    }
}