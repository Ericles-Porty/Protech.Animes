namespace Protech.Animes.Application.Interfaces;

public interface ICryptographyService
{
    string Encrypt(string text);
    bool Validate(string text, string hashedText);

    byte[] ConvertToBytes(string text);
    string ConvertToString(byte[] bytes);
}