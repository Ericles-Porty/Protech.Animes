namespace Protech.Animes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Protech.Animes.Infrastructure.Entities;

public class ProtechAnimesDbContext : DbContext
{

    public DbSet<Anime> Animes { get; set; }
    public DbSet<Director> Directors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anime>()
            .HasOne(a => a.Director)
            .WithMany(d => d.Animes)
            .HasForeignKey(a => a.DirectorId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProtechAnimeDb;Username=postgres;Password=123456");
        }
    }
}