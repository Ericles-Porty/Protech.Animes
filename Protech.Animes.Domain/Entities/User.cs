namespace Protech.Animes.Domain.Entities;

public class User
{
    public string Id { get; set; } = null!;

    public required string UserName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public string Role { get; set; } = "User";

}