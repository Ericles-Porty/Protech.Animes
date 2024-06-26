﻿// using Microsoft.Extensions.Options;
// using Moq;
// using Protech.Animes.Application.Configurations;
// using Protech.Animes.Application.DTOs;
// using Protech.Animes.Application.Interfaces;
// using Protech.Animes.Application.Services;
// using Protech.Animes.Domain.Entities;
// using Protech.Animes.Domain.Exceptions;

// namespace Protech.Animes.Application.UseCases.AuthUseCases.Tests;

// [TestClass()]
// public class RegisterUserUseCaseTests
// {
//     [TestMethod()]
//     public void ExecuteTest()
//     {
//         // Arrange
//         IOptions<JwtConfig> jwtConfig = Options.Create(new JwtConfig
//         {
//             Secret = "fa9e3f36571b12d9ab882f76e5f8e30c3d3fcb7ec0968a8a519398e0b5125324",
//             Issuer = "Issuer",
//             Audience = "Audience",
//             DurationInHours = 1
//         });
//         var jwtTokenService = new JwtTokenService(jwtConfig);

//         var userToRegister = new RegisterUserDto
//         {
//             Name = "John Doe",
//             Email = "JohnDoe@gmail.com",
//             Password = "123456",
//             ConfirmPassword = "123456"
//         };

//         var cryptographyService = new CryptographyService();
//         var encryptedPassword = cryptographyService.Encrypt(userToRegister.Password);
//         var encryptedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(encryptedPassword);

//         var userEntity = new User
//         {
//             Id = Guid.NewGuid(),
//             Name = userToRegister.Name,
//             Email = userToRegister.Email,
//             Password = encryptedPasswordBytes
//         };


//         var token = jwtTokenService.GenerateToken(userEntity);

//         var userServiceMock = new Mock<IUserService>();
//         userServiceMock.Setup(x => x.Register(It.IsAny<User>())).ReturnsAsync(userEntity);

//         var useCase = new RegisterUserUseCase(userServiceMock.Object, jwtTokenService, cryptographyService);

//         // Act
//         var result = useCase.Execute(userToRegister).Result;


//         // Assert
//         Assert.IsNotNull(result);
//         Assert.IsNotNull(userEntity);

//         Assert.AreEqual(userEntity.Name, result.Name);
//         Assert.AreEqual(userEntity.Email, result.Email);

//         Assert.AreEqual(token, result.Token);

//         userServiceMock.Verify(x => x.Register(It.IsAny<User>()), Times.Once);
//     }

//     [TestMethod()]
//     public void ExecuteTest_PasswordsDoNotMatch()
//     {
//         // Arrange
//         var userToRegister = new RegisterUserDto
//         {
//             Name = "John Doe",
//             Email = "JohnDoe@gmail.com",
//             Password = "123456",
//             ConfirmPassword = "1234567"
//         };

//         var userServiceMock = new Mock<IUserService>();
//         userServiceMock.Setup(x => x.Register(It.IsAny<User>())).Verifiable();

//         var jwtTokenServiceMock = new Mock<IJwtTokenService>();
//         jwtTokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Verifiable();

//         var cryptographyServiceMock = new Mock<ICryptographyService>();
//         cryptographyServiceMock.Setup(x => x.Encrypt(It.IsAny<string>())).Verifiable();

//         var useCase = new RegisterUserUseCase(userServiceMock.Object, jwtTokenServiceMock.Object, cryptographyServiceMock.Object);

//         // Act & Assert
//         Assert.ThrowsExceptionAsync<BadRequestException>(() => useCase.Execute(userToRegister));

//         // Assert
//         userServiceMock.Verify(x => x.Register(It.IsAny<User>()), Times.Never);
//         jwtTokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Never);
//         cryptographyServiceMock.Verify(x => x.Encrypt(It.IsAny<string>()), Times.Never);

//     }

    
// }