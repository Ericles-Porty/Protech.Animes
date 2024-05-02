namespace Protech.Animes.Domain.Policies;

public static class RateLimitPolicies
{
    public const string Fixed = "fixed";
    public const string LoginAttempts = "login-attempts";
}