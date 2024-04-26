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
        await _dbContext.Animes.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<Anime?> CreateAnimeWithDirectorAsync(Anime anime, Director director)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var createdDirector = await _dbContext.Directors.AddAsync(director);
            await _dbContext.SaveChangesAsync();

            anime.DirectorId = createdDirector.Entity.Id;

            await _dbContext.Animes.AddAsync(anime);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception("An error occurred while creating the anime with director transaction", ex);
        }

        return anime;
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

    public async Task<Anime?> UpdateAnimeWithNewDirectorAsync(Anime anime, Director director)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var createdDirector = await _dbContext.Directors.AddAsync(director);
            await _dbContext.SaveChangesAsync();

            anime.DirectorId = createdDirector.Entity.Id;

            var updatedAnime = _dbContext.Animes.Update(anime);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return updatedAnime.Entity;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            throw new Exception("An error occurred while updating the anime with director transaction", ex);
        }
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

    public async Task<IEnumerable<Anime>> GetByDirectorNameAsync(string directorName)
    {
        return await FilterByDirectorName(directorName).ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByDirectorNamePaginatedAsync(string directorName, int page, int pageSize)
    {
        return await FilterByDirectorName(directorName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByKeywordSummaryAsync(string keyword)
    {
        return await FilterBySummaryKeyword(keyword).ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByKeywordSummaryPaginatedAsync(string keyword, int page, int pageSize)
    {
        return await FilterBySummaryKeyword(keyword)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Anime?> GetByNameAsync(string name)
    {
        return await _dbContext.Animes
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.Name == name);
    }

    public async Task<IEnumerable<Anime>> GetByNamePatternAsync(string name)
    {
        return await FilterByNamePattern(name).ToListAsync();
    }

    public async Task<IEnumerable<Anime>> GetByNamePatternPaginatedAsync(string name, int page, int pageSize)
    {
        return await FilterByNamePattern(name)
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

    private IQueryable<Anime> FilterByDirectorName(string directorName)
    {
        return _dbContext.Animes
            .AsNoTracking()
            .Where(a => EF.Functions.ILike(a.Director.Name, $"%{directorName}%"))
            .Include(a => a.Director);
    }

    private IQueryable<Anime> FilterByNamePattern(string name)
    {
        return _dbContext.Animes
            .AsNoTracking()
            .Where(a => EF.Functions.ILike(a.Name, $"%{name}%"))
            .Include(a => a.Director);
    }

    private IQueryable<Anime> FilterBySummaryKeyword(string keyword)
    {
        return _dbContext.Animes
            .AsNoTracking()
            .Where(a => EF.Functions.ILike(a.Summary, $"%{keyword}%"))
            .Include(a => a.Director);
    }
}