using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries.Handlers;

public class GetAnimesBySummaryKeywordHandler : IRequestHandler<GetAnimesBySummaryKeywordQuery, IEnumerable<AnimeDto>>
{
    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public GetAnimesBySummaryKeywordHandler(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnimeDto>> Handle(GetAnimesBySummaryKeywordQuery request, CancellationToken cancellationToken)
    {
        if (!request.PaginationParams.Page.HasValue || !request.PaginationParams.PageSize.HasValue)
        {
            var animes = await _animeService.GetAnimesBySummaryKeyword(request.Keyword);
            var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);
            return animesDto;
        }

        if (request.PaginationParams.Page <= 0 || request.PaginationParams.PageSize <= 0)
            throw new BadRequestException("Invalid page or page size");

        var animesPaginated = await _animeService.GetAnimesBySummaryKeywordPaginated(request.Keyword, request.PaginationParams.Page.Value, request.PaginationParams.PageSize.Value);
        var animesDtoPaginated = _mapper.Map<IEnumerable<AnimeDto>>(animesPaginated);
        return animesDtoPaginated;
    }
}