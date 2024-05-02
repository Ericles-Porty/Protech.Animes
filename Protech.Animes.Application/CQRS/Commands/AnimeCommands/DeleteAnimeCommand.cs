using MediatR;

namespace Protech.Animes.Application.CQRS.Commands.AnimeCommands;
public class DeleteAnimeCommand(int id) : IRequest<bool>
{
    public int Id { get; set; } = id;
}