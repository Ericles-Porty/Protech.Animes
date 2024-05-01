// using AutoMapper;
// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;
// using Protech.Animes.Domain.Entities;
// using Protech.Animes.Domain.Exceptions;
// using Protech.Animes.Domain.Interfaces.Repositories;

// namespace Protech.Animes.Application.UseCases.AnimeUseCases;

// public class CreateAnimeUseCase
// {
//     private readonly IAnimeService _animeService;
//     private readonly IDirectorRepository _directorRepository;
//     private readonly IMapper _mapper;

//     public CreateAnimeUseCase(IAnimeService animeService, IDirectorRepository directorRepository, IMapper mapper)
//     {
//         _animeService = animeService;
//         _directorRepository = directorRepository;
//         _mapper = mapper;
//     }

//     public async Task<AnimeDto> Execute(CreateAnimeDto createAnimeDto)
//     {
//         var animeDto = _mapper.Map<AnimeDto>(createAnimeDto);

//         var animeAlreadyExists = await _animeService.GetAnimeByName(animeDto.Name);
//         if (animeAlreadyExists is not null) throw new DuplicatedEntityException("Anime", "Name");

//         Director? director = await _directorRepository.GetByNameAsync(animeDto.DirectorName);

//         if (director is null)
//         {
//             var anime = await _animeService.CreateAnimeWithDirector(animeDto);
//             return anime;
//         }

//         animeDto.DirectorId = director.Id;
//         var createdAnimeDto = await _animeService.CreateAnime(animeDto);

//         return createdAnimeDto;

//     }
// }