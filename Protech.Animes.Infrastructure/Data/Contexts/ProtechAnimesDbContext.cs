namespace Protech.Animes.Infrastructure.Data.Contexts;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Infrastructure.Data.Configurations;

public class ProtechAnimesDbContext : IdentityDbContext
{
    public ProtechAnimesDbContext() { }
    public ProtechAnimesDbContext(DbContextOptions<ProtechAnimesDbContext> options) : base(options) { }

    public DbSet<Anime> Animes { get; set; }
    public DbSet<Director> Directors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DirectorConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnimeConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}