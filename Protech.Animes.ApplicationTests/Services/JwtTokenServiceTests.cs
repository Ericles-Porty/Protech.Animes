// using Microsoft.Extensions.Options;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Protech.Animes.Application.Configurations;
// using Protech.Animes.Application.Services;
// using Protech.Animes.Domain.Entities;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Protech.Animes.Application.Services.Tests;

// [TestClass()]
// public class JwtTokenServiceTests
// {
//     [TestMethod()]
//     public void GenerateTokenTest()
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

//         var password = "123456";
//         var passwordBytes = Encoding.UTF8.GetBytes(password);
//         var user = new User
//         {
//             Id = Guid.NewGuid(),
//             Name = "User",
//             Email = "User@gmail.com",
//             Password = passwordBytes,
//             Role = "user"
//         };

//         // Act
//         var token = jwtTokenService.GenerateToken(user);

//         // Assert
//         Assert.IsNotNull(token);
//     }

// }