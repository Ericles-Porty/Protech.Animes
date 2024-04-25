using System.ComponentModel.DataAnnotations;

namespace Protech.Animes.Application.DTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "The Name is required")]
    [StringLength(100, ErrorMessage = "The Name must have between 3 and 100 characters", MinimumLength = 3)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The Email is required")]
    [EmailAddress(ErrorMessage = "The Email is invalid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "The Password is required")]
    [StringLength(100, ErrorMessage = "The Password must have between 6 and 100 characters", MinimumLength = 6)]
    public required string Password { get; set; }

    [Required(ErrorMessage = "The Confirm Password is required")]
    [Compare("Password", ErrorMessage = "The Password and Confirm Password must be equals")]
    public required string ConfirmPassword { get; set; }
}