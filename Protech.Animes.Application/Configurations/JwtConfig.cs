namespace Protech.Animes.Application.Configurations;

public class JwtConfig
{
    public required string Secret { get; set; }

    public required string Issuer { get; set; }

    public required string Audience { get; set; }

    public required int DurationInHours { get; set; }

    public required int RefreshDurationInDays { get; set; }
}