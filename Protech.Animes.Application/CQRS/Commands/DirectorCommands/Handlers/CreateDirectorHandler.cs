using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.DirectorCommands.Handlers;

public class CreateDirectorHandler : IRequestHandler<CreateDirectorCommand, DirectorDto>
{
    private readonly IDirectorService _directorService;
    private readonly IMapper _mapper;

    public CreateDirectorHandler(IDirectorService directorService, IMapper mapper)
    {
        _directorService = directorService;
        _mapper = mapper;
    }

    public async Task<DirectorDto> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = _mapper.Map<Director>(request);
        var directorCreated = await _directorService.CreateDirector(director);
        var directorDto = _mapper.Map<DirectorDto>(directorCreated);
        return directorDto;
    }
}