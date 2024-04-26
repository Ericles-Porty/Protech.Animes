using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.Interfaces;

public interface IDirectorService
{
    Task<IEnumerable<DirectorDto>> GetDirectors();

    Task<DirectorDto> GetDirector(int id);

    Task<DirectorDto> GetDirectorByName(string name);

    Task<DirectorDto> CreateDirector(CreateDirectorDto createDirectorDto);

    Task<DirectorDto> UpdateDirector(int id, UpdateDirectorDto updateDirectorDto);

    Task<bool> DeleteDirector(int id);
}