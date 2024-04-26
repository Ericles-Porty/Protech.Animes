using System.ComponentModel.DataAnnotations;

namespace Protech.Animes.Application.DTOs;
public class AnimeDto
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Summary { get; set; }

    public int DirectorId { get; set; }

    public required string DirectorName { get; set; }
}