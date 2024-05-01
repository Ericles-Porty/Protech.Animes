using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.Commands;

public class LoginUserCommand(LoginUserDto loginUserDto) : IRequest<UserDto>
{
    public LoginUserDto LoginUserDto { get; } = loginUserDto;

}