// using Moq;
// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;
// using Protech.Animes.Domain.Exceptions;

// namespace Protech.Animes.Application.UseCases.AnimeUseCases.Tests;

// [TestClass()]
// public class GetAnimeUseCaseTests
// {


//     [TestMethod()]
//     public void ExecuteTest()
//     {
//         // Arrange
//         var anime = new AnimeDto
//         {
//             Id = 1,
//             Name = "Naruto",
//             Summary = "Naruto Uzumaki é um menino que vive em Konohagakure no Sato ou simplesmente Konoha ou Vila Oculta da Folha, a vila ninja do País do Fogo.",
//             DirectorName = "Hayato Date"
//         };

//         var animeServiceMock = new Mock<IAnimeService>();
//         animeServiceMock.Setup(x => x.GetAnime(It.IsAny<int>())).ReturnsAsync(anime);

//         var useCase = new GetAnimeUseCase(animeServiceMock.Object);

//         // Act
//         var result = useCase.Execute(1).Result;

//         // Assert
//         Assert.IsNotNull(result);
//         Assert.IsNotNull(anime);

//         Assert.AreEqual(anime.Id, result.Id);
//         Assert.AreEqual(anime.Name, result.Name);
//         Assert.AreEqual(anime.Summary, result.Summary);
//         Assert.AreEqual(anime.DirectorName, result.DirectorName);

//         animeServiceMock.Verify(x => x.GetAnime(It.IsAny<int>()), Times.Once);
//     }

//     [TestMethod()]
//     public void ExecuteTest_InvalidId()
//     {
//         // Arrange
//         var animeServiceMock = new Mock<IAnimeService>();
//         animeServiceMock.Setup(x => x.GetAnime(It.IsAny<int>())).ThrowsAsync(new NotFoundException("Anime not found"));

//         var useCase = new GetAnimeUseCase(animeServiceMock.Object);

//         // Act & Assert
//         Assert.ThrowsExceptionAsync<NotFoundException>(() => useCase.Execute(1));

//         animeServiceMock.Verify(x => x.GetAnime(It.IsAny<int>()), Times.Once);
//     }
// }