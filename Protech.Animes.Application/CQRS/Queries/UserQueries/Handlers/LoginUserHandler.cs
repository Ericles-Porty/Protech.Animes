using MediatR;
using System.Security.Authentication;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Protech.Animes.Application.CQRS.Queries.UserQueries.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, UserDto>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginUserHandler(
        IJwtTokenService jwtTokenService,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
        )
    {
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<UserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) throw new InvalidCredentialException("Invalid credentials.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password,  false);
        if (result.IsLockedOut) throw new InvalidCredentialException("Account is locked.");
        if (!result.Succeeded) throw new InvalidCredentialException("Invalid credentials.");

        var jwtToken = await _jwtTokenService.GenerateToken(user.Email!);
        var refreshToken = await _jwtTokenService.GenerateRefreshToken(user.Email!);
        var userDto = new UserDto
        {
            Name = user.UserName!,
            Email = user.Email!,
            Token = jwtToken,
            RefreshToken = refreshToken
        };
        return userDto;
    }
}