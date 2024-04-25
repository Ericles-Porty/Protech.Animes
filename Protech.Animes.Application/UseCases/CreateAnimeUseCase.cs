using AutoMapper;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Infrastructure.Data.Repositories.Interfaces;

namespace Protech.Animes.Application.UseCases;

public class CreateAnimeUseCase
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IDirectorRepository _directorRepository;
    private readonly IMapper _mapper;

    public CreateAnimeUseCase(IAnimeRepository animeRepository, IDirectorRepository directorRepository, IMapper mapper)
    {
        _animeRepository = animeRepository;
        _directorRepository = directorRepository;
        _mapper = mapper;
    }

    public async Task<AnimeDto> Execute(CreateAnimeDto animeDto)
    {
        Director director;

        var existingDirector = await _directorRepository.GetByNameAsync(animeDto.DirectorName);


        if (existingDirector is not null)
        {
            director = existingDirector;
        }
        else
        {
            var directorDto = new DirectorDto { Name = animeDto.DirectorName };

            var mappedDirector = _mapper.Map<Director>(directorDto);

            var createdDirector = await _directorRepository.CreateAsync(mappedDirector);

            director = createdDirector;
        }


        var anime = _mapper.Map<Anime>(animeDto);

        anime.Director = director;

        var createdAnime = await _animeRepository.CreateAsync(anime);

        var createdAnimeDto = _mapper.Map<AnimeDto>(createdAnime);

        return createdAnimeDto;
    }
}