using Protech.Animes.Application.Interfaces;

namespace Protech.Animes.Application.Services;

public class CryptographyService : ICryptographyService
{
    private static readonly int _saltSize = 12;

    public byte[] ConvertToBytes(string text)
    {
        return System.Text.Encoding.UTF8.GetBytes(text);
    }

    public string ConvertToString(byte[] bytes)
    {
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    public bool Validate(string text, string hashedText)
    {
        return BCrypt.Net.BCrypt.Verify(text, hashedText);
    }

    public string Encrypt(string text)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(_saltSize);
        var hashedText = BCrypt.Net.BCrypt.HashPassword(text, salt);
        return hashedText;
    }
}