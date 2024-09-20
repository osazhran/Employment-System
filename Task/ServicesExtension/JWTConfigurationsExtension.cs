using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.ConfigurationData;
using System.Text;

namespace API.ServicesExtension;
public static class JWTConfigurationsExtension
{
    public static IServiceCollection AddJWTConfigurations(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var jwtData = serviceProvider.GetRequiredService<IOptions<JWTData>>().Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = jwtData.ValidAudience,
                ValidateIssuer = true,
                ValidIssuer = jwtData.ValidIssuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtData.SecretKey)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}