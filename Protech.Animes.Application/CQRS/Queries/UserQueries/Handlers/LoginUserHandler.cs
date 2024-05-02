using MediatR;
using System.Security.Authentication;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Interfaces.Services;

namespace Protech.Animes.Application.CQRS.Queries.UserQueries.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, UserDto>
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICryptographyService _cryptographyService;

    public LoginUserHandler(IUserService userService, IJwtTokenService jwtTokenService, ICryptographyService cryptographyService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _cryptographyService = cryptographyService;
    }

    public async Task<UserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmail(request.Email);
        if (user is null) throw new InvalidCredentialException("Invalid credentials.");

        var userPassword = System.Text.Encoding.UTF8.GetString(user.Password);
        var isValidPassword = _cryptographyService.Validate(request.Password, userPassword);
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