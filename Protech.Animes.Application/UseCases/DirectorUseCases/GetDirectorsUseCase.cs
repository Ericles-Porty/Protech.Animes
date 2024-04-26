using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.UseCases.DirectorUseCases;

public class GetDirectorsUseCase
{

    private readonly IDirectorService _directorService;

    public GetDirectorsUseCase(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<IEnumerable<DirectorDto>> Execute() =>
        await _directorService.GetDirectors();


}