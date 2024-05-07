using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Protech.Animes.Domain.Entities;

namespace Protech.Animes.Infrastructure.Data.Configurations;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.ToTable("Directors");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedOnAdd();

        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(d => d.Name).IsUnique();

        // Erro no driver do postgresql ao tentar inserir dados com HasData
        // builder.HasData(
        //         new Director { Id = 1, Name = "Mamoru Hosoda" },
        //         new Director { Id = 2, Name = "Makoto Shinkai" },
        //         new Director { Id = 3, Name = "Hayao Miyazaki" },
        //         new Director { Id = 4, Name = "Isao Takahata" },
        //         new Director { Id = 5, Name = "Katsuhiro Otomo" },
        //         new Director { Id = 6, Name = "Satoshi Kon" },
        //         new Director { Id = 7, Name = "Masaaki Yuasa" },
        //         new Director { Id = 8, Name = "Makoto Shinkai 2" },
        //         new Director { Id = 9, Name = "Hideaki Anno" },
        //         new Director { Id = 10, Name = "Kunihiko Ikuhara" },
        //         new Director { Id = 11, Name = "Mamoru Oshii" },
        //         new Director { Id = 12, Name = "Yoshiyuki Tomino" },
        //         new Director { Id = 13, Name = "Gisaburō Sugii" },
        //         new Director { Id = 14, Name = "Rintaro" },
        //         new Director { Id = 15, Name = "Yoshiaki Kawajiri" },
        //         new Director { Id = 16, Name = "Kazuki Akane" },
        //         new Director { Id = 17, Name = "Tatsuyuki Nagai" },
        //         new Director { Id = 18, Name = "Takashi Nakamura" }
        // );
    }
}