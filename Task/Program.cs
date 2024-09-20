using API.ServicesExtension;
using Microsoft.EntityFrameworkCore;
using Repository.Identity;
using Serilog;

#region Add services to the container
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();
builder.Services.ConfigureAppsettingData(builder.Configuration);

builder.Services.AddIdentityConfigurations();

builder.Services.AddJWTConfigurations();

builder.Services.AddRedis();

builder.Services.AddFluentValidation();

builder.Services.AddApplicationServices();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration);
});

#endregion

#region Update Database With Using Way And Seeding Data
var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;


var _identiyContext = services.GetRequiredService<IdentityContext>();
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await _identiyContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error has been occured during apply the migration!");
}

#endregion

#region Configure the Kestrel pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMiddleware();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion

