using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries.Handlers;

public class GetAnimeByIdHandler : IRequestHandler<GetAnimeByIdQuery, AnimeDto>
{
    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public GetAnimeByIdHandler(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<AnimeDto> Handle(GetAnimeByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            throw new ArgumentException("Id must be greater than 0");

        var anime = await _animeService.GetAnime(request.Id);
        var animeDto = _mapper.Map<AnimeDto>(anime);
        return animeDto;
    }
}