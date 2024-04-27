using System.ComponentModel.DataAnnotations;
using Protech.Animes.Application.Validations;

namespace Protech.Animes.Application.DTOs;

public class CreateDirectorDto
{
    [Required(ErrorMessage = "Name is required")]
    [HumanNamePattern]
    public required string Name { get; set; }

}