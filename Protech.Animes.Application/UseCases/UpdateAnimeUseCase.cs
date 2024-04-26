using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.UseCases;

public class UpdateAnimeUseCase
{

    private readonly IAnimeService _animeService;
    private readonly IDirectorService _directorService;
    private readonly IMapper _mapper;

    public UpdateAnimeUseCase(IAnimeService animeService, IDirectorService directorService, IMapper mapper)
    {
        _animeService = animeService;
        _directorService = directorService;
        _mapper = mapper;
    }

    public async Task<AnimeDto> Execute(int id, UpdateAnimeDto updateAnimeDto)
    {
        if (updateAnimeDto.Id != id) throw new BadRequestException("Id is not compatible with the request");

        var anime = await _animeService.GetAnime(id);
        if (anime is null) throw new NotFoundException("Anime not found");

        if (updateAnimeDto.DirectorName != anime.DirectorName || updateAnimeDto.DirectorId != anime.DirectorId)
            throw new BadRequestException("Director not compatible with the request");

        var animeWithSameName = await _animeService.GetAnimeByName(updateAnimeDto.Name);
        if (animeWithSameName is not null && animeWithSameName.Id != id)
            throw new DuplicatedEntityException("Anime", "Name");

        var animeDto = _mapper.Map<AnimeDto>(updateAnimeDto);

        var director = await _directorService.GetDirector(updateAnimeDto.DirectorId);
        if (director is null)
        {
            var updatedAnimeDto = await _animeService.UpdateAnimeWithNewDirector(animeDto);

            return updatedAnimeDto;
        }

        var updatedAnime = await _animeService.UpdateAnime(id, animeDto);

        return updatedAnime;
    }

}