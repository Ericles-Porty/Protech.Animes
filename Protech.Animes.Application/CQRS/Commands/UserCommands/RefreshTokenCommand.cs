
using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Commands.UserCommands;

public class RefreshTokenCommand : IRequest<UserDto>
{
    public required string Token { get; set; }
}