using MediatR;

namespace Protech.Animes.Application.CQRS.Commands.DirectorCommands;

public class DeleteDirectorCommand(int id) : IRequest<bool>
{
    public int Id { get; set; } = id;
}