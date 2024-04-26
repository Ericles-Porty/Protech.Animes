using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Protech.Animes.Application.Configurations;
using Protech.Animes.Application.DTOs;
using Protech.Animes.Application.Interfaces;
using Protech.Animes.Application.Services;
using Protech.Animes.Application.UseCases.AuthUseCases;
using Protech.Animes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Protech.Animes.Application.UseCases.AuthUseCases.Tests;

[TestClass()]
public class LoginUserUseCaseTests
{
    [TestMethod()]
    public void LoginUserUseCaseTest()
    {
        // Arrange
        IOptions<JwtConfig> jwtConfig = Options.Create(new JwtConfig
        {
            Secret = "fa9e3f36571b12d9ab882f76e5f8e30c3d3fcb7ec0968a8a519398e0b5125324",
            Issuer = "Issuer",
            Audience = "Audience",
            DurationInHours = 1
        });

        var loginUserDto = new LoginUserDto
        {
            Email = "JohnDoe@gmail.com",
            Password = "123456"
        };

        var userEntity = new User
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = loginUserDto.Email,
            Password = loginUserDto.Password
        };
        var jwtTokenService = new JwtTokenService(jwtConfig);

        var token = jwtTokenService.GenerateToken(userEntity);

        var userDto = new UserDto
        {
            Name = userEntity.Name,
            Email = userEntity.Email,
            Token = token
        };

        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(userEntity);

        var jwtTokenServiceMock = new Mock<IJwtTokenService>();
        jwtTokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns(token);

        var useCase = new LoginUserUseCase(userServiceMock.Object, jwtTokenServiceMock.Object);

        // Act
        var result = useCase.Execute(loginUserDto.Email, loginUserDto.Password).Result;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(userDto.Name, result.Name);
        Assert.AreEqual(userDto.Email, result.Email);
        Assert.AreEqual(userDto.Token, result.Token);

        userServiceMock.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        jwtTokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Once);

    }

    [TestMethod()]
    public void LoginUserUseCaseTest_UserNotFound()
    {
        // Arrange
        var loginUserDto = new LoginUserDto
        {
            Email = "JohnDoe@gmail.com",
            Password = "1234567"
        };

        User? user = null;

        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);

        var jwtTokenServiceMock = new Mock<IJwtTokenService>();
        jwtTokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Verifiable();

        var useCase = new LoginUserUseCase(userServiceMock.Object, jwtTokenServiceMock.Object);

        // Act & Assert
        Assert.ThrowsExceptionAsync<InvalidCredentialException>(() => useCase.Execute(loginUserDto.Email, loginUserDto.Password));

        userServiceMock.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        jwtTokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Never);
    }
}
