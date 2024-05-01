
using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Queries.DirectorQueries.Handlers;

public class GetDirectorsByIdHandler : IRequestHandler<GetDirectorByIdQuery, DirectorDto>
{
    private readonly IDirectorService _directorService;
    private readonly IMapper _mapper;

    public GetDirectorsByIdHandler(IDirectorService directorService, IMapper mapper)
    {
        _directorService = directorService;
        _mapper = mapper;
    }

    public async Task<DirectorDto> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id < 1)
            throw new ArgumentException("Id must be greater than 0");

        var director = await _directorService.GetDirector(request.Id);
        var directorDto = _mapper.Map<DirectorDto>(director);
        return directorDto;
    }
}