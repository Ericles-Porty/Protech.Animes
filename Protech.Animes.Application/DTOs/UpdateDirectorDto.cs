using System.ComponentModel.DataAnnotations;
using Protech.Animes.Application.Validations;

namespace Protech.Animes.Application.DTOs;

public class UpdateDirectorDto
{
    [Required(ErrorMessage = "Id is required")]
    public required int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [HumanNamePattern]
    public required string Name { get; set; }

}