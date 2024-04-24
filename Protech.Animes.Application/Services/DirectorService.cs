using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

namespace Protech.Animes.Application.Services;

public class DirectorService : IDirectorService
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IMapper _mapper;

    public DirectorService(IDirectorRepository directorRepository, IMapper mapper)
    {
        _directorRepository = directorRepository;
        _mapper = mapper;
    }

    public async Task<DirectorDto> CreateDirector(DirectorDto directorDto)
    {
        Console.WriteLine("CreateDirector");
        var director = _mapper.Map<Director>(directorDto);

        var createdDirector = await _directorRepository.CreateAsync(director);

        var directorDtoCreated = _mapper.Map<DirectorDto>(createdDirector);

        Console.WriteLine("Director created");
        return directorDtoCreated;
    }

    public async Task<bool> DeleteDirector(int id)
    {
        return await _directorRepository.DeleteAsync(id);
    }

    public async Task<DirectorDto> GetDirector(int id)
    {
        var director = await _directorRepository.GetByIdAsync(id);

        if (director is null) throw new NotFoundException("Director not found");

        var directorDto = _mapper.Map<DirectorDto>(director);

        return directorDto;
    }

    public async Task<IEnumerable<DirectorDto>> GetDirectors()
    {
        var directors = await _directorRepository.GetAllAsync();

        var directorsDto = _mapper.Map<IEnumerable<DirectorDto>>(directors);

        return directorsDto;
    }

    public async Task<DirectorDto> UpdateDirector(int id, DirectorDto directorDto)
    {
        var director = _mapper.Map<Director>(directorDto);

        var updatedDirector = await _directorRepository.UpdateAsync(id, director);

        if (updatedDirector is null) throw new NotFoundException("Director not found");

        var directorDtoUpdated = _mapper.Map<DirectorDto>(updatedDirector);

        return directorDtoUpdated;
    }

    public async Task<DirectorDto> GetDirectorByName(string name)
    {
        var director = await _directorRepository.GetByNameAsync(name);

        if (director is null) throw new NotFoundException("Director not found");

        var directorDto = _mapper.Map<DirectorDto>(director);

        return directorDto;
    }
}

