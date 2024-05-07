namespace Protech.Animes.Domain.Entities;

public class Director
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public List<Anime>? Animes { get; set; }

}