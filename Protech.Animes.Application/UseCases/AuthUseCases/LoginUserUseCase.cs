using System.Security.Authentication;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.UseCases.AuthUseCases;

public class LoginUserUseCase
{

    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICryptographyService _cryptographyService;

    public LoginUserUseCase(IUserService userService, IJwtTokenService jwtTokenService, ICryptographyService cryptographyService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _cryptographyService = cryptographyService;
    }

    public async Task<UserDto> Execute(string email, string password)
    {
        var user = await _userService.GetUserByEmail(email);
        if (user is null) throw new InvalidCredentialException("Invalid credentials.");

        var userPassword = System.Text.Encoding.UTF8.GetString(user.Password);
        var isValidPassword = _cryptographyService.Validate(password, userPassword);
        if (!isValidPassword) throw new InvalidCredentialException("Invalid credentials.");

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