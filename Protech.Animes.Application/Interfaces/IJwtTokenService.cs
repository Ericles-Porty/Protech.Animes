using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    
}