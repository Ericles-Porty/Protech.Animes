using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Protech.Animes.Application.Configurations;
using Protech.Animes.Domain.Interfaces.Services;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Protech.Animes.Application.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtConfig _jwtConfig;
    private readonly UserManager<IdentityUser> _userManager;

    public JwtTokenService(IOptions<JwtConfig> jwtConfig, UserManager<IdentityUser> userManager)
    {
        _jwtConfig = jwtConfig.Value;
        _userManager = userManager;
    }

    public async Task<string> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new Exception("UserNotFound");

        var userRoles = await _userManager.GetRolesAsync(user);

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));
        identityClaims.AddClaims(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty));
        identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            Subject = identityClaims,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Expires = DateTime.UtcNow.AddHours(_jwtConfig.DurationInHours),
            IssuedAt = DateTime.UtcNow,
            TokenType = "at+jwt"
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<string> GenerateRefreshToken(string email)
    {
        var jti = Guid.NewGuid().ToString();
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, jti),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            Subject = identityClaims,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(_jwtConfig.RefreshDurationInDays),
            IssuedAt = DateTime.UtcNow,
            TokenType = "rt+jwt"
        };

        await UpdateLastGeneratedClaim(email, jti);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task UpdateLastGeneratedClaim(string email, string jti)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new Exception("UserNotFound");

        var claims = await _userManager.GetClaimsAsync(user);
        var newLastRefreshTokenClaim = new Claim("LastRefreshToken", jti);

        var claimLastRefreshToken = claims.FirstOrDefault(f => f.Type == "LastRefreshToken");

        if (claimLastRefreshToken is null)
            await _userManager.AddClaimAsync(user, newLastRefreshTokenClaim);
        else
            await _userManager.ReplaceClaimAsync(user, claimLastRefreshToken, newLastRefreshTokenClaim);
    }
}