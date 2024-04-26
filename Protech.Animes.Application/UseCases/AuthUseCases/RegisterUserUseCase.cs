using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.UseCases.AuthUseCases;

public class RegisterUserUseCase
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterUserUseCase(IUserService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<UserDto> Execute(RegisterUserDto createdUserDto)
    {
        if (createdUserDto.Password != createdUserDto.ConfirmPassword) throw new BadRequestException("Passwords do not match");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = createdUserDto.Name,
            Email = createdUserDto.Email,
            Password = createdUserDto.Password,
        };

        var createdUser = await _userService.Register(user);

        var jwtToken = _jwtTokenService.GenerateToken(createdUser);

        var userDto = new UserDto
        {
            Name = createdUser.Name,
            Email = createdUser.Email,
            Token = jwtToken
        };

        return userDto;
    }
}