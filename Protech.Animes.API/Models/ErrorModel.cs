
namespace Protech.Animes.API.Models;

public class ErrorModel
{
    public required int StatusCode { get; set; }
    public required string Message { get; set; }
}