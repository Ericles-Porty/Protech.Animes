using AutoMapper;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.DirectorCommands.Handlers;

public class UpdateDirectorHandler : IRequestHandler<UpdateDirectorCommand, DirectorDto>
{
    private readonly IDirectorService _directorService;
    private readonly IMapper _mapper;

    public UpdateDirectorHandler(IDirectorService directorService, IMapper mapper)
    {
        _directorService = directorService;
        _mapper = mapper;
    }

    public async Task<DirectorDto> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
    {
        if (request.Id < 1)
            throw new ArgumentException("Id must be greater than 0");

        var director = await _directorService.GetDirector(request.Id);
        if (director == null)
            throw new NotFoundException("Director not found");

        director.Name = request.Name;

        var directorUpdated = await _directorService.UpdateDirector(director);
        var directorDto = _mapper.Map<DirectorDto>(directorUpdated);
        return directorDto;
    }
}