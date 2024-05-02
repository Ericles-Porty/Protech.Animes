using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Protech.Animes.Domain.Policies;

namespace Protech.Animes.API.Extensions.RateLimit;

public static class RateLimitExtension
{
    public static IServiceCollection AddRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(
            options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy(
                    policyName: RateLimitPolicies.LoginAttempts,
                    partitioner: httpContext => RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                        factory: (key) => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromMinutes(10)
                        }
                    )
                );

                options.AddFixedWindowLimiter(
                    policyName: RateLimitPolicies.Fixed,
                    configureOptions: rateLimitOptions =>
                    {
                        rateLimitOptions.PermitLimit = 100;
                        rateLimitOptions.Window = TimeSpan.FromMinutes(10);
                    }
                );
            }
        );

        return services;
    }
}