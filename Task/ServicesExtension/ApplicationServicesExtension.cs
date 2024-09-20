using Core.Interfaces;
using Core.Interfaces.Repositories;
using DotNetCore_ECommerce.Helpers;
using Repository;
using Service;

namespace API.ServicesExtension;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<IVacancyService, VacancyService>();

        services.AddScoped<IApplicationService, ApplicationService>();

        services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));

        services.AddAutoMapper(typeof(MappingProfiles));

        return services;
    }
}