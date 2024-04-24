namespace Protech.Animes.Infrastructure.Data.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Protech.Animes.Infrastructure.Data.Contexts;
using Protech.Animes.Infrastructure.Data.Repositories.Interfaces;
using Protech.Animes.Domain.Entities;

public class AnimeRepository : IAnimeRepository
{

    private readonly ProtechAnimesDbContext _dbContext;

    public AnimeRepository(ProtechAnimesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Anime> CreateAsync(Anime entity)
    {
        Console.WriteLine("Creating anime");
        Console.WriteLine(entity);

        await _dbContext.Animes.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        Console.WriteLine("Anime created");
        Console.WriteLine(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var anime = await GetByIdAsync(id);

        if (anime is null) return false;

        _dbContext.Animes.Remove(anime);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Anime>> GetAllAsync()
    {
        return await _dbContext.Animes
            .Include(a => a.Director)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Anime?> GetByIdAsync(int id)
    {
        return await _dbContext.Animes.FindAsync(id);
    }

    public async Task<Anime?> GetByIdIncludingDirectorAsync(int id)
    {
        return await _dbContext.Animes
            .Include(a => a.Director)
            .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Anime?> UpdateAsync(int id, Anime entity)
    {
        var anime = await GetByIdAsync(id);

        if (anime is null) return null;

        anime.Name = entity.Name;
        anime.Summary = entity.Summary;
        anime.DirectorId = entity.DirectorId;
        await _dbContext.SaveChangesAsync();

        return anime;
    }

    public async Task<IEnumerable<Anime>> GetAllPaginatedAsync(int page, int pageSize)
    {
        return await _dbContext.Animes
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByDirectorIdAsync(int directorId)
    {
        return await GetByDirectorIdQuery(directorId).ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByDirectorIdPaginatedAsync(int directorId, int page, int pageSize)
    {
        return await GetByDirectorIdQuery(directorId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByKeywordSummaryAsync(string keyword)
    {
        return await _dbContext.Animes
            .AsNoTracking()
            .Where(a => a.Summary.Contains(keyword))
            .ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByKeywordSummaryPaginatedAsync(string keyword, int page, int pageSize)
    {
        return await _dbContext.Animes
            .AsNoTracking()
            .Where(a => a.Summary.Contains(keyword))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    private IQueryable<Anime> GetByDirectorIdQuery(int directorId)
    {
        return _dbContext.Animes
            .AsNoTracking()
            .Where(a => a.DirectorId == directorId)
            .Include(a => a.Director);
    }
}