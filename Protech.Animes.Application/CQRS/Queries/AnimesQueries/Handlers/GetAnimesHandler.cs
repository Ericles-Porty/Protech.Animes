using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries.Handlers;

public class GetAnimesHandler : IRequestHandler<GetAnimesQuery, IEnumerable<AnimeDto>>
{
    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public GetAnimesHandler(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnimeDto>> Handle(GetAnimesQuery request, CancellationToken cancellationToken)
    {
        if (!request.PaginationParams.Page.HasValue || !request.PaginationParams.PageSize.HasValue)
        {
            var animes = await _animeService.GetAnimes();
            var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);
            return animesDto;
        }

        if (request.PaginationParams.Page.Value <= 0 || request.PaginationParams.PageSize.Value <= 0)
            throw new ArgumentException("Page and PageSize must be greater than 0");

        var animesPaginated = await _animeService.GetAnimesPaginated(request.PaginationParams.Page.Value, request.PaginationParams.PageSize.Value);
        var animesDtoPaginated = _mapper.Map<IEnumerable<AnimeDto>>(animesPaginated);
        return animesDtoPaginated;
    }
}