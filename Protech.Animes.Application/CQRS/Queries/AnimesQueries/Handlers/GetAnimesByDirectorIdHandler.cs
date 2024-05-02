using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Queries.AnimesQueries.Handlers;

public class GetAnimesByDirectorIdHandler : IRequestHandler<GetAnimesByDirectorIdQuery, IEnumerable<AnimeDto>>
{
    private readonly IAnimeService _animeService;
    private readonly IMapper _mapper;

    public GetAnimesByDirectorIdHandler(IAnimeService animeService, IMapper mapper)
    {
        _animeService = animeService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnimeDto>> Handle(GetAnimesByDirectorIdQuery request, CancellationToken cancellationToken)
    {
        if (!request.PaginationParams.Page.HasValue || !request.PaginationParams.PageSize.HasValue)
        {
            var animes = await _animeService.GetAnimesByDirector(request.DirectorId);
            var animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);
            return animesDto;
        }

        if (request.PaginationParams.Page <= 0 || request.PaginationParams.PageSize <= 0)
            throw new BadRequestException("Invalid page or page size");

        var animesPaginated = await _animeService.GetAnimesByDirectorPaginated(request.DirectorId, request.PaginationParams.Page.Value, request.PaginationParams.PageSize.Value);
        var animesDtoPaginated = _mapper.Map<IEnumerable<AnimeDto>>(animesPaginated);
        return animesDtoPaginated;
    }
}