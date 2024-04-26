using AutoMapper;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.UseCases.Anime;

public class DeleteAnimeUseCase
{

    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public DeleteAnimeUseCase(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<bool> Execute(int id)
    {
        var anime = await _animeService.GetAnime(id);

        if (anime is null) throw new NotFoundException("Anime not found");

        var deleted = await _animeService.DeleteAnime(id);

        return deleted;
    }
}