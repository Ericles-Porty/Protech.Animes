using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.Commands;

public class RegisterUserCommand(RegisterUserDto registerUserDto) : IRequest<UserDto>
{
    public RegisterUserDto RegisterUserDto { get; } = registerUserDto;
}