using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Protech.Animes.Application.DTOs;

public class DirectorDto
{
    [JsonIgnore]
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
}