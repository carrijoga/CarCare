using CarCare.Application.Authentication;

namespace CarCare.Server.Services;

public static class AddServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<AuthApplication>();
        services.AddScoped<TokenAuthApplication>();

        return services;
    }
}