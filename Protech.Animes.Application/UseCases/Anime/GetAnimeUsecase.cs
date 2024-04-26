using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.UseCases.Anime;

public class GetAnimeUsecase(IAnimeService animeService)
{

    private readonly IAnimeService _animeService = animeService;

    public async Task<AnimeDto> Execute(int id) =>
        await _animeService.GetAnime(id);

}