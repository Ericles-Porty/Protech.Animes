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
        Console.WriteLine("Creating anime use case");

        var existingDirector = await _directorRepository.GetByNameAsync(animeDto.DirectorName);
        Console.WriteLine("Existing director");
        Console.WriteLine(existingDirector);


        if (existingDirector is not null)
        {
            Console.WriteLine("Director already exists");
            director = existingDirector;
        }
        else
        {
            Console.WriteLine("Creating director");
            var directorDto = new DirectorDto { Name = animeDto.DirectorName };

            Console.WriteLine("Director DTO");
            Console.WriteLine(directorDto);

            var mappedDirector = _mapper.Map<Director>(directorDto);

            Console.WriteLine("Mapped director");
            Console.WriteLine(mappedDirector);

            var createdDirector = await _directorRepository.CreateAsync(mappedDirector);

            Console.WriteLine("Created director");
            Console.WriteLine(createdDirector);

            director = createdDirector;
        }

        Console.WriteLine("Director created");
        Console.WriteLine(director);

        var anime = _mapper.Map<Anime>(animeDto);

        anime.Director = director;

        Console.WriteLine("Creating anime");
        Console.WriteLine(anime);

        var createdAnime = await _animeRepository.CreateAsync(anime);

        Console.WriteLine("Anime created");
        Console.WriteLine(createdAnime);

        var createdAnimeDto = _mapper.Map<AnimeDto>(createdAnime);

        return createdAnimeDto;
    }
}