using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.UseCases.AnimeUseCases;

public class UpdateAnimeUseCase
{

    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public UpdateAnimeUseCase(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<AnimeDto> Execute(int id, UpdateAnimeDto updateAnimeDto)
    {
        if (updateAnimeDto.Id != id) throw new BadRequestException("Id does not match.");

        var anime = await _animeService.GetAnime(id);
        if (anime is null) throw new NotFoundException("Anime not found");

        var animeWithSameName = await _animeService.GetAnimeByName(updateAnimeDto.Name);
        if (animeWithSameName is not null && animeWithSameName.Id != id) throw new DuplicatedEntityException("Anime", "Name");

        var animeDto = new AnimeDto
        {
            Id = anime.Id,
            Name = updateAnimeDto.Name,
            Summary = updateAnimeDto.Summary,
            DirectorId = anime.DirectorId,
            DirectorName = anime.DirectorName
        };

        var updatedAnime = await _animeService.UpdateAnime(id, animeDto);

        return updatedAnime;
    }

}