using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.UseCases.DirectorUseCases;

public class CreateDirectorUseCase
{
    private readonly IDirectorService _directorService;

    public CreateDirectorUseCase(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<DirectorDto> Execute(CreateDirectorDto createDirectorDto)
    {
        var directorDto = await _directorService.GetDirectorByName(createDirectorDto.Name);
        if (directorDto is not null) throw new DuplicatedEntityException("Director", "Name");

        return await _directorService.CreateDirector(createDirectorDto);
    }
}
