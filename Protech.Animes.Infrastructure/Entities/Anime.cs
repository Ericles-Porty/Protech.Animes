using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protech.Animes.Infrastructure.Entities;

public class Anime
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required string Summary { get; set; }

    public required Director Director { get; set; }

    public int DirectorId { get; set; }
}