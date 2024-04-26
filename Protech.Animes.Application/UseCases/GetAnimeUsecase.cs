using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Application.UseCases;

public class GetAnimeUsecase(IAnimeService animeService)
{

    private readonly IAnimeService _animeService = animeService;

    public async Task<AnimeDto> Execute(int id) =>
        await _animeService.GetAnime(id);

}