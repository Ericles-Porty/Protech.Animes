using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Domain.Exceptions;

namespace Protech.Animes.Application.UseCases.AuthUseCases;

public class RegisterUserUseCase
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICryptographyService _cryptographyService;

    public RegisterUserUseCase(IUserService userService, IJwtTokenService jwtTokenService, ICryptographyService cryptographyService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _cryptographyService = cryptographyService;
    }

    public async Task<UserDto> Execute(RegisterUserDto createdUserDto)
    {
        if (createdUserDto.Password != createdUserDto.ConfirmPassword) throw new BadRequestException("Passwords do not match");

        var hashedPassword = _cryptographyService.Encrypt(createdUserDto.Password);
        var hashedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(hashedPassword);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = createdUserDto.Name,
            Email = createdUserDto.Email,
            Password = hashedPasswordBytes
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