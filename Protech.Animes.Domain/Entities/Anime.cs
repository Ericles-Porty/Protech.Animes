namespace Protech.Animes.Domain.Entities;

public class Anime
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Summary { get; set; }
    public required Director Director { get; set; }
    public int DirectorId { get; set; }
}