using System.ComponentModel.DataAnnotations;
using MediatR;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Validations;

namespace Protech.Animes.Application.CQRS.Commands.DirectorCommands;

public class CreateDirectorCommand : IRequest<DirectorDto>
{
    [Required(ErrorMessage = "Name is required")]
    [HumanNamePattern]
    public required string Name { get; set; }
}