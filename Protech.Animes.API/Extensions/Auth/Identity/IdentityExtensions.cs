using Microsoft.AspNetCore.Identity;
using Protech.Animes.Infrastructure.Data.Contexts;

namespace Protech.Animes.API.Extensions.Auth.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityExtension(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.SignIn.RequireConfirmedEmail = false;
        })
            .AddEntityFrameworkStores<ProtechAnimesDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}