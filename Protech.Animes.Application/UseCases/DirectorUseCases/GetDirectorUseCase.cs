using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.UseCases.DirectorUseCases;

public class GetDirectorUseCase
{

    private readonly IDirectorService _directorService;

    public GetDirectorUseCase(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<DirectorDto> Execute(int id) =>
        await _directorService.GetDirector(id);
        
}