namespace Protech.Animes.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Protech.Animes.Domain.Entities;
using Protech.Animes.Infrastructure.Data.Seed;

public class ProtechAnimesDbContext : DbContext
{

    public ProtechAnimesDbContext() { }

    public ProtechAnimesDbContext(DbContextOptions<ProtechAnimesDbContext> options) : base(options) { }

    public DbSet<Anime> Animes { get; set; }

    public DbSet<Director> Directors { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anime>()
            .HasOne(a => a.Director)
            .WithMany(d => d.Animes)
            .HasForeignKey(a => a.DirectorId);

        modelBuilder.Entity<Anime>()
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder.Entity<Director>()
            .HasIndex(d => d.Name)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasDefaultValue("User");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Id);

        modelBuilder.SeedData();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProtechAnimeDb;Username=postgres;Password=123456");
        }
    }
}