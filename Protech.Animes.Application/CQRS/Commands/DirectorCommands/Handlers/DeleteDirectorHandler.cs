using MediatR;
using Protech.Animes.Domain.Exceptions;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Commands.DirectorCommands.Handlers;

public class DeleteDirectorHandler : IRequestHandler<DeleteDirectorCommand, bool>
{
    private readonly IDirectorService _directorService;

    public DeleteDirectorHandler(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    public async Task<bool> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            throw new ArgumentException("Id must be greater than 0");

        var director = await _directorService.GetDirector(request.Id);
        if (director == null)
            throw new NotFoundException("Director not found");

        var directorDeleted = await _directorService.DeleteDirector(request.Id);
        return directorDeleted;
    }
}