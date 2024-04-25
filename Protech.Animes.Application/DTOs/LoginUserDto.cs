using System.ComponentModel.DataAnnotations;

namespace Protech.Animes.Application.DTOs;

public class LoginUserDto
{
    [Required(ErrorMessage = "The Email is required")]
    [EmailAddress(ErrorMessage = "The Email is invalid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "The Password is required")]
    [StringLength(100, ErrorMessage = "The Password must have between 6 and 100 characters", MinimumLength = 6)]
    public required string Password { get; set; }

}