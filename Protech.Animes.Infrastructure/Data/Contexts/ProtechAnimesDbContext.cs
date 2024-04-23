namespace Protech.Animes.Infrastructure.Data.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Protech.Animes.Infrastructure.Entities;

    public class ProtechAnimesDbContext : DbContext
    {
        public ProtechAnimesDbContext(DbContextOptions<ProtechAnimesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Anime> Animes { get; set; }
        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>()
                .HasOne(a => a.Director)
                .WithMany()
                .HasForeignKey(a => a.DirectorId);
        }
    }
}