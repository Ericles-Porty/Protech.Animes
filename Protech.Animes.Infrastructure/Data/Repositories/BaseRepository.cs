using Microsoft.EntityFrameworkCore;
using Protech.Animes.Domain.Interfaces.Repositories;
using Protech.Animes.Infrastructure.Data.Contexts;

namespace Protech.Animes.Infrastructure.Data.Repositories;

public abstract class BaseRepository<T, K> : IBaseRepository<T, K> where T : class where K : struct
{
    private readonly ProtechAnimesDbContext _dbContext;

    public BaseRepository(ProtechAnimesDbContext context)
    {
        _dbContext = context;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsyncWithouTracking()
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(K id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(K id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return false;

        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    protected IQueryable<T> Paginate(IQueryable<T> query, int page, int pageSize)
    {
        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}