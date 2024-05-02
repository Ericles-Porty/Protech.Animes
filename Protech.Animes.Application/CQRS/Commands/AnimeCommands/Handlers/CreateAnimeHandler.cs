using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.AnimeCommands.Handlers;

public class CreateAnimeHandler : IRequestHandler<CreateAnimeCommand, AnimeDto>
{
    private readonly IAnimeService _animeService;
    private readonly IDirectorService _directorService;
    private readonly IMapper _mapper;

    public CreateAnimeHandler(IAnimeService animeService, IDirectorService directorService, IMapper mapper)
    {
        _animeService = animeService;
        _directorService = directorService;
        _mapper = mapper;
    }

    public async Task<AnimeDto> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
    {
        if (request.DirectorId <= 0) throw new ArgumentException("Invalid director id");

        var animeAlreadyExists = await _animeService.GetAnimeByName(request.Name);
        if (animeAlreadyExists is not null) throw new DuplicatedEntityException("Anime already exists");

        var director = await _directorService.GetDirector(request.DirectorId);
        if (director is null) throw new NotFoundException("Director not found");

        var anime = _mapper.Map<Anime>(request);
        anime.Director = director;
        var animeCreated = await _animeService.CreateAnime(anime);
        var animeDto = _mapper.Map<AnimeDto>(animeCreated);
        return animeDto;
    }
}