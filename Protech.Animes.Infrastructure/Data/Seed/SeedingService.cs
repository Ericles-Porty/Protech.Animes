// using Microsoft.EntityFrameworkCore;
// using Protech.Animes.Domain.Entities;

// namespace Protech.Animes.Infrastructure.Data.Seed;

// public static class SeedingService
// {
//     public static void SeedData(this ModelBuilder modelBuilder)
//     {
//         var d1 = new Director { Id = 1, Name = "Mamoru Hosoda" };
//         var d2 = new Director { Id = 2, Name = "Makoto Shinkai" };
//         var d3 = new Director { Id = 3, Name = "Hayao Miyazaki" };
//         var d4 = new Director { Id = 4, Name = "Isao Takahata" };
//         var d5 = new Director { Id = 5, Name = "Katsuhiro Otomo" };
//         var d6 = new Director { Id = 6, Name = "Satoshi Kon" };
//         var d7 = new Director { Id = 7, Name = "Masaaki Yuasa" };
//         var d8 = new Director { Id = 8, Name = "Makoto Shinkai 2" };
//         var d9 = new Director { Id = 9, Name = "Hideaki Anno" };
//         var d10 = new Director { Id = 10, Name = "Kunihiko Ikuhara" };
//         var d11 = new Director { Id = 11, Name = "Mamoru Oshii" };
//         var d12 = new Director { Id = 12, Name = "Yoshiyuki Tomino" };
//         var d13 = new Director { Id = 13, Name = "Gisaburō Sugii" };
//         var d14 = new Director { Id = 14, Name = "Rintaro" };
//         var d15 = new Director { Id = 15, Name = "Yoshiaki Kawajiri" };
//         var d16 = new Director { Id = 16, Name = "Kazuki Akane" };
//         var d17 = new Director { Id = 17, Name = "Tatsuyuki Nagai" };
//         var d18 = new Director { Id = 18, Name = "Takashi Nakamura" };

//         var a1 = new Anime
//         {
//             Id = 1,
//             Name = "Wolf Children",
//             Summary = "Wolf Children is a 2012 Japanese anime film directed and co-written by Mamoru Hosoda.",
//             DirectorId = 1,
//             Director = d1
//         };
//         var a2 = new Anime
//         {
//             Id = 2,
//             Name = "Weathering with You",
//             Summary = "Weathering with You is a 2019 Japanese animated romantic fantasy film written and directed by Makoto Shinkai.",
//             DirectorId = 2,
//             Director = d2
//         };
//         var a3 = new Anime
//         {
//             Id = 3,
//             Name = "Spirited Away",
//             Summary = "Spirited Away is a 2001 Japanese animated fantasy film written and directed by Hayao Miyazaki.",
//             DirectorId = 3,
//             Director = d3
//         };
//         var a4 = new Anime
//         {
//             Id = 4,
//             Name = "Grave of the Fireflies",
//             Summary = "Grave of the Fireflies is a 1988 Japanese animated war tragedy film written and directed by Isao Takahata.",
//             DirectorId = 4,
//             Director = d4
//         };
//         var a5 = new Anime
//         {
//             Id = 5,
//             Name = "Akira",
//             Summary = "Akira is a 1988 Japanese animated post-apocalyptic cyberpunk film directed by Katsuhiro Otomo.",
//             DirectorId = 5,
//             Director = d5
//         };
//         var a6 = new Anime
//         {
//             Id = 6,
//             Name = "Perfect Blue",
//             Summary = "Perfect Blue is a 1997 Japanese animated psychological thriller film directed by Satoshi Kon.",
//             DirectorId = 6,
//             Director = d6
//         };
//         var a7 = new Anime
//         {
//             Id = 7,
//             Name = "Devilman Crybaby",
//             Summary = "Devilman Crybaby is a Japanese original net animation (ONA) anime series based on Go Nagai's manga series Devilman.",
//             DirectorId = 7,
//             Director = d7
//         };
//         var a8 = new Anime
//         {
//             Id = 8,
//             Name = "Your Name",
//             Summary = "Your Name is a 2016 Japanese animated romantic fantasy film written and directed by Makoto Shinkai.",
//             DirectorId = 8,
//             Director = d8
//         };
//         var a9 = new Anime
//         {
//             Id = 9,
//             Name = "Neon Genesis Evangelion",
//             Summary = "Neon Genesis Evangelion is a Japanese mecha anime television series produced by Gainax and Tatsunoko Production.",
//             DirectorId = 9,
//             Director = d9
//         };

//         modelBuilder.Entity<Director>().HasData(
//             d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15, d16, d17, d18
//         );

//         modelBuilder.Entity<Anime>().HasData(
//             a1, a2, a3, a4, a5, a6, a7, a8, a9
//         );

//     }
// }