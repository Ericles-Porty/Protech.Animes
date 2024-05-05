
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Protech.Animes.Application.Configurations;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Domain.Interfaces.Services;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Protech.Animes.Application.CQRS.Commands.UserCommands.Handlers;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, UserDto>
{
    private readonly JwtConfig _jwtConfig;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly UserManager<IdentityUser> _userManager;

    public RefreshTokenHandler(
        IOptions<JwtConfig> jwtConfig,
        IJwtTokenService jwtTokenService,
        UserManager<IdentityUser> userManager
        )
    {
        _jwtConfig = jwtConfig.Value;
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
    }

    public async Task<UserDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var result = await tokenHandler.ValidateTokenAsync(request.Token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _jwtConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtConfig.Audience,
            ValidateLifetime = false
        });

        if (!result.IsValid) throw new AuthenticationException("Invalid token");

        var user = await _userManager.FindByEmailAsync(result.Claims[JwtRegisteredClaimNames.Email].ToString() ?? string.Empty);
        if (user == null) throw new AuthenticationException("Invalid token");

        var claims = await _userManager.GetClaimsAsync(user);

        if (!claims.Any(c => c.Type == "LastRefreshToken" && c.Value == result.Claims[JwtRegisteredClaimNames.Jti].ToString()))
            throw new AuthenticationException("Expired token");

        if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.Now)
            throw new AuthenticationException("User blocked");

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