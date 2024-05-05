namespace Protech.Animes.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    Task<string> GenerateToken(string email);
    Task<string> GenerateRefreshToken(string email);
}