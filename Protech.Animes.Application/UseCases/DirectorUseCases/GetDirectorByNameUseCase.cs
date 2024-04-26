using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.UseCases.DirectorUseCases;

public class GetDirectorByNameUseCase
{
    private readonly IDirectorService _directorService;

    public GetDirectorByNameUseCase(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<DirectorDto> Execute(string name) =>
        await _directorService.GetDirectorByName(name);

}