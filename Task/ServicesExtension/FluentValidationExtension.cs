using Core.DtosValidator;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.ServicesExtension;
public static class FluentValidationExtension
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<RegisterRequestValidator>()
            .AddValidatorsFromAssemblyContaining<LoginRequestValidator>()
            .AddValidatorsFromAssemblyContaining<ApplicationRequestValidator>()
            .AddValidatorsFromAssemblyContaining<VacancyRequestValidator>();

        return services;
    }
}