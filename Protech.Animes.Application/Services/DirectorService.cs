using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Repositories;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.Services;

public class DirectorService : IDirectorService
{
    private readonly IDirectorRepository _directorRepository;

    public DirectorService(IDirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }

    public async Task<Director> CreateDirector(Director director)
    {
        var createdDirector = await _directorRepository.CreateAsync(director);
        return createdDirector;
    }

    public async Task<bool> DeleteDirector(int id)
    {
        return await _directorRepository.DeleteAsync(id);
    }

    public async Task<Director> GetDirector(int id)
    {
        var director = await _directorRepository.GetByIdAsync(id);
        if (director is null) throw new NotFoundException("Director not found");

        return director;
    }

    public async Task<IEnumerable<Director>> GetDirectors()
    {
        return await _directorRepository.GetAllAsync();
    }

    public async Task<Director> UpdateDirector(int id, Director director)
    {
        var updatedDirector = await _directorRepository.UpdateAsync(director);
        if (updatedDirector is null) throw new NotFoundException("Director not found");

        return updatedDirector;
    }

    public async Task<Director?> GetDirectorByName(string name)
    {
        var director = await _directorRepository.GetByNameAsync(name);
        if (director is null) return null;

        return director;
    }

    public async Task<IEnumerable<Director>> GetDirectorsByNamePattern(string name)
    {
        return await _directorRepository.GetByNamePatternAsync(name);
    }

    public async Task<IEnumerable<Director>> GetDirectorsPaginated(int page, int pageSize)
    {
        return await _directorRepository.GetAllPaginatedAsync(page, pageSize);
    }

    public async Task<IEnumerable<Director>> GetDirectorsByNamePatternPaginated(string name, int page, int pageSize)
    {
        return await _directorRepository.GetByNamePatternPaginatedAsync(name, page, pageSize);
    }
}

