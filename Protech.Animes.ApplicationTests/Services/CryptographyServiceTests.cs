using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protech.Animes.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protech.Animes.Application.Services.Tests;
[TestClass()]
public class CryptographyServiceTests
{
    [TestMethod()]
    public void ValidateTest()
    {
        // Arrange
        var service = new CryptographyService();
        var password = "123456";
        var hash = service.Encrypt(password);

        // Act
        var result = service.Validate(password, hash);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod()]
    public void ValidateTest_WrongPassword()
    {
        // Arrange
        var service = new CryptographyService();
        var password = "123456";
        var hash = service.Encrypt(password);

        var wrongPassword = "1234567";

        // Act
        var result = service.Validate(wrongPassword, hash);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod()]
    public void EncryptTest()
    {
        // Arrange
        var service = new CryptographyService();
        var password = "123456";

        // Act
        var result = service.Encrypt(password);

        // Assert
        Assert.IsNotNull(result);

    }
}