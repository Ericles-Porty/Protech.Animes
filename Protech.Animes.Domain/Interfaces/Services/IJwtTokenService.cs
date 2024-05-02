using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}