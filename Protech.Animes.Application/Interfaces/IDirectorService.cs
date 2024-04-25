using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.Interfaces;

public interface IDirectorService
{
    Task<IEnumerable<DirectorDto>> GetDirectors();

    Task<DirectorDto> GetDirector(int id);

    Task<DirectorDto> GetDirectorByName(string name);

    Task<DirectorDto> CreateDirector(DirectorDto directorDto);

    Task<DirectorDto> UpdateDirector(int id, DirectorDto directorDto);

    Task<bool> DeleteDirector(int id);
}