using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.UseCases.DirectorUseCases;

public class UpdateDirectorUseCase
{
    private readonly IDirectorService _directorService;

    public UpdateDirectorUseCase(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<DirectorDto> Execute(int id, UpdateDirectorDto updateDirectorDto)
    {
        if (id != updateDirectorDto.Id) throw new BadRequestException("Id does not match.");

        var director = await _directorService.GetDirector(id);
        if (director == null) throw new NotFoundException("Director not found.");

        return await _directorService.UpdateDirector(id, updateDirectorDto);
    }
}