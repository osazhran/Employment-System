using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Identity;
using Service.ConfigurationData;

namespace API.ServicesExtension;
public static class IdentityConfigurationsExtension
{
	public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
	{
		
		var serviceProvider = services.BuildServiceProvider();
		var databaseConnections = serviceProvider.GetRequiredService<IOptions<DatabaseConnections>>().Value;

		services.AddDbContext<IdentityContext>(options =>
		{
			options.UseSqlServer(databaseConnections.IdentityConnection);
		});
        ///Allow DI For Identity Service 
        ///AddIdentity----> Stor Roles And Identity For Users
        services.AddIdentity<AppUser, IdentityRole>(option =>
		{
			option.Password.RequireLowercase = true;
			option.Password.RequireUppercase = false;
			option.Password.RequireDigit = false;
			option.Password.RequireNonAlphanumeric = true;
			option.Password.RequiredUniqueChars = 3;
			option.Password.RequiredLength = 6;
		}).AddEntityFrameworkStores<IdentityContext>();// EFcore.يربط خدمات الهوية بقاعدة البيانات باستخدام
        return services;
	}
}

/*
This part connects the Identity system to the database using Entity Framework. 
The IdentityContext is a DbContext that represents your connection to the database.
It means that all user and role data will be stored in the database via Entity Framework, 
using the tables that Identity automatically creates (such as AspNetUsers and AspNetRoles).
*/
/*
  بحيث اني معملش تعريف لكل DbContext 
  services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
});
 ************************************************************************
 services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("RedisConnection"));
});
 
 */