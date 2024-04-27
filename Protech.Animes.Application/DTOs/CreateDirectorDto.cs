using System.ComponentModel.DataAnnotations;

namespace Protech.Animes.Application.DTOs;

public class CreateDirectorDto
{
    [Required(ErrorMessage = "Name is required")]
    [Length(3, 100, ErrorMessage = "Name must have between 3 and 100 characters")]
    public required string Name { get; set; }

}