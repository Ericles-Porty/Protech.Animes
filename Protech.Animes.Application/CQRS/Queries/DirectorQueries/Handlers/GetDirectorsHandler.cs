
using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Queries.DirectorQueries.Handlers;

public class GetDirectorsHandler : IRequestHandler<GetDirectorsQuery, IEnumerable<DirectorDto>>
{
    private readonly IMapper _mapper;
    private readonly IDirectorService _directorService;

    public GetDirectorsHandler(IDirectorService directorService, IMapper mapper)
    {
        _directorService = directorService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DirectorDto>> Handle(GetDirectorsQuery request, CancellationToken cancellationToken)
    {
        if (!request.Page.HasValue || !request.PageSize.HasValue)
        {
            var directors = await _directorService.GetDirectors();
            var directorsDto = _mapper.Map<IEnumerable<DirectorDto>>(directors);
            return directorsDto;
        }

        if (request.Page.Value < 1 || request.PageSize.Value < 1)
            throw new ArgumentException("Page and pageSize must be greater than 0");

        var directorsPaginated = await _directorService.GetDirectorsPaginated(request.Page.Value, request.PageSize.Value);
        var directorsPaginatedDto = _mapper.Map<IEnumerable<DirectorDto>>(directorsPaginated);
        return directorsPaginatedDto;
    }
}