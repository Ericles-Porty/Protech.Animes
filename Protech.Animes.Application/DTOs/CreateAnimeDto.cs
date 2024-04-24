using System.ComponentModel.DataAnnotations;

namespace Protech.Animes.Application.DTOs;

public class CreateAnimeDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name is too long")]
    [MinLength(3, ErrorMessage = "Name is too short")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Summary is required")]
    [StringLength(2000, ErrorMessage = "Summary is too long")]
    [MinLength(3, ErrorMessage = "Summary is too short")]
    public required string Summary { get; set; }

    [Required(ErrorMessage = "Director name is required")]
    [StringLength(100, ErrorMessage = "Director name is too long")]
    [MinLength(3, ErrorMessage = "Director name is too short")]
    public required string DirectorName { get; set; }
}