using Service.ConfigurationData;

namespace API.ServicesExtension;
public static class ConfigurationClassesExtension
{
    public static IServiceCollection ConfigureAppsettingData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConnections>(configuration.GetSection("ConnectionStrings"));

        services.Configure<JWTData>(configuration.GetSection("JWT"));

        return services;
    }
}