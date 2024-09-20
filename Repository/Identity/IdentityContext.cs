using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository.Identity;
public class IdentityContext: IdentityDbContext<AppUser>//This Class Inhert Form DbContext + Scurity module
{
	public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);//First Call Dbset For IdentityDbContext User,Roles,...7 Dbset
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());// Call My Config
    }
    public DbSet<Vacancy> Vacancies { get; set; } 
    public DbSet<Application> Applications { get; set; }
}