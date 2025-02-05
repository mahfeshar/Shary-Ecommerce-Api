
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shary.API.Extensions;
using Shary.API.Middlewares;
using Shary.Core.Entities.Identity;
using Shary.Core.Repositories.Contract;
using Shary.Repository;
using Shary.Repository.Data;
using Shary.Repository.Identity;
using StackExchange.Redis;

namespace Shary.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddSwaggerServices();

        builder.Services.AddDbContext<StoreContext>(options 
            => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDbContext<AppIdentityDbContext>(options
            => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

        builder.Services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            string? connection = builder.Configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(connection);
        });
        
        builder.Services.AddApplicationServices();
        builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", options =>
            {
                options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
            });
        });

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var _dbContext = services.GetService<StoreContext>();
        var loggerFactory = services.GetService<ILoggerFactory>();
        var _identityContext = services.GetService<AppIdentityDbContext>();
        var _userManager = services.GetRequiredService<UserManager<AppUser>>();

        try
        {
            await _dbContext.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(_dbContext);
            await _identityContext.Database.MigrateAsync();
            await AppIdentityDbContextSeed.SeedUsersAsync(_userManager);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "Error during updating migration");
        }

        #region Configure Kestrel Middlewares
        // Configure the HTTP request pipeline.
        
        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerMiddlewares();
        }

        app.UseStatusCodePagesWithReExecute("/Errors/{0}");

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.MapControllers();

        app.UseCors("MyPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run(); 
        #endregion
    }
}
