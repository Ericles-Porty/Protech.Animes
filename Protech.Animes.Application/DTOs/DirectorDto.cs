using System.ComponentModel.DataAnnotations;

namespace Protech.Animes.Application.DTOs;

public class DirectorDto
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
}