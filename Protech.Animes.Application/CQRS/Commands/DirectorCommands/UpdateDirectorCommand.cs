using System.ComponentModel.DataAnnotations;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Validations;

namespace Protech.Animes.Application.CQRS.Commands.DirectorCommands;

public class UpdateDirectorCommand : IRequest<DirectorDto>
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [HumanNamePattern]
    public required string Name { get; set; }
}