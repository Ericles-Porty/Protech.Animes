using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Protech.Animes.API.Extensions.Auth.JWT;

public static class JwtExtensions
{
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var configKey = configuration["JwtConfig:Secret"];

        var key = Encoding.ASCII.GetBytes(configKey ?? throw new ArgumentNullException("JwtConfig:Secret is required"));

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}