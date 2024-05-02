using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.AnimeCommands.Handlers;

public class CreateAnimeHandler : IRequestHandler<CreateAnimeCommand, AnimeDto>
{
    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public CreateAnimeHandler(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<AnimeDto> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
    {
        var anime = _mapper.Map<Anime>(request);
        var animeCreated = await _animeService.CreateAnime(anime);
        var animeDto = _mapper.Map<AnimeDto>(animeCreated);
        return animeDto;
    }
}