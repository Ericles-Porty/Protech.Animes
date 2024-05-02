namespace Protech.Animes.Domain.Interfaces.Services;

public interface ICryptographyService
{
    string Encrypt(string text);
    bool Validate(string text, string hashedText);
}