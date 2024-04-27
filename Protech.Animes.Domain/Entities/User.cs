using System.ComponentModel.DataAnnotations;

namespace Protech.Animes.Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required byte[] Password { get; set; }

    public string Role { get; set; } = "User";

}