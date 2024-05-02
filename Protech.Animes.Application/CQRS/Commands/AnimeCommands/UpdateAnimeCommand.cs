using System.ComponentModel.DataAnnotations;
using MediatR;
using Protech.Animes.Application.DTOs;

namespace Protech.Animes.Application.CQRS.Commands.AnimeCommands;

public class UpdateAnimeCommand : IRequest<AnimeDto>
{
    [Required(ErrorMessage = "Id is required")]
    public required int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name is too long")]
    [MinLength(3, ErrorMessage = "Name is too short")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Summary is required")]
    [StringLength(2000, ErrorMessage = "Summary is too long")]
    [MinLength(3, ErrorMessage = "Summary is too short")]
    public required string Summary { get; set; }
}