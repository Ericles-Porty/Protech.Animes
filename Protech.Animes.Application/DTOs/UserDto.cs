namespace Protech.Animes.Application.DTOs;

public class UserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}