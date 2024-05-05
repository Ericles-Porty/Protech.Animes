
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Infrastructure.Data.Configurations;

public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
{
    public void Configure(EntityTypeBuilder<Anime> builder)
    {
        builder.ToTable("Animes");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(a => a.Name).IsUnique();

        builder.Property(a => a.Summary).IsRequired().HasMaxLength(500);

        builder.Property(a => a.DirectorId).IsRequired();

        builder.HasOne(a => a.Director)
            .WithMany(d => d.Animes)
            .HasForeignKey(a => a.DirectorId);

        // Erro no driver do postgresql ao tentar inserir dados com HasData
        // builder.HasData(
        //     new Anime
        //     {
        //         Id = 1,
        //         Name = "Wolf Children",
        //         Summary = "Wolf Children is a 2012 Japanese anime film directed and co-written by Mamoru Hosoda.",
        //         DirectorId = 1
        //     },
        //     new Anime
        //     {
        //         Id = 2,
        //         Name = "Weathering with You",
        //         Summary = "Weathering with You is a 2019 Japanese animated romantic fantasy film written and directed by Makoto Shinkai.",
        //         DirectorId = 2
        //     },
        //     new Anime
        //     {
        //         Id = 3,
        //         Name = "Spirited Away",
        //         Summary = "Spirited Away is a 2001 Japanese animated fantasy film written and directed by Hayao Miyazaki.",
        //         DirectorId = 3
        //     },
        //     new Anime
        //     {
        //         Id = 4,
        //         Name = "Grave of the Fireflies",
        //         Summary = "Grave of the Fireflies is a 1988 Japanese animated war tragedy film written and directed by Isao Takahata.",
        //         DirectorId = 4
        //     },
        //     new Anime
        //     {
        //         Id = 5,
        //         Name = "Akira",
        //         Summary = "Akira is a 1988 Japanese animated post-apocalyptic cyberpunk film directed by Katsuhiro Otomo.",
        //         DirectorId = 5
        //     },
        //     new Anime
        //     {
        //         Id = 6,
        //         Name = "Perfect Blue",
        //         Summary = "Perfect Blue is a 1997 Japanese animated psychological thriller film directed by Satoshi Kon.",
        //         DirectorId = 6
        //     },
        //     new Anime
        //     {
        //         Id = 7,
        //         Name = "Devilman Crybaby",
        //         Summary = "Devilman Crybaby is a Japanese original net animation (ONA) anime series based on Go Nagai's manga series Devilman.",
        //         DirectorId = 7
        //     },
        //     new Anime
        //     {
        //         Id = 8,
        //         Name = "Neon Genesis Evangelion",
        //         Summary = "Neon Genesis Evangelion is a Japanese mecha anime television series produced by Gainax and Tatsunoko Production.",
        //         DirectorId = 8
        //     },
        //     new Anime
        //     {
        //         Id = 9,
        //         Name = "Your Name",
        //         Summary = "Your Name is a 2016 Japanese animated romantic fantasy film written and directed by Makoto Shinkai.",
        //         DirectorId = 9
        //     }
        // );
    }
}