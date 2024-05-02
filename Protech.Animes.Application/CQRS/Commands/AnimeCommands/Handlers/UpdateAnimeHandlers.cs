using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.AnimeCommands.Handlers;

public class UpdateAnimeHandlers : IRequestHandler<UpdateAnimeCommand, AnimeDto>
{
    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public UpdateAnimeHandlers(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<AnimeDto> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            throw new ArgumentException("Id must be greater than 0");

        var anime = _mapper.Map<Anime>(request);
        var updatedAnime = await _animeService.UpdateAnime(anime);
        var updatedAnimeDto = _mapper.Map<AnimeDto>(updatedAnime);
        return updatedAnimeDto;
    }
}