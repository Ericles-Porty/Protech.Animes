using System.Security.Authentication;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.UseCases;

public class LoginUserUseCase
{

    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginUserUseCase(IUserService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<UserDto> Execute(string email, string password)
    {
        var user = await _userService.Login(email, password);

        if (user is null) throw new InvalidCredentialException("User not found");

        var jwtToken = _jwtTokenService.GenerateToken(user);

        var userDto = new UserDto
        {
            Name = user.Name,
            Email = user.Email,
            Token = jwtToken
        };

        return userDto;
    }

}