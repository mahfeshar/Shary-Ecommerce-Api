using Microsoft.AspNetCore.Identity;
using Shary.Core.Entities.Identity;
using Shary.Core.Services.Contract;
using Shary.Repository.Identity;
using Shary.Service;
using System.Runtime.CompilerServices;

namespace Shary.API.Extensions;

public static class IdentityServicesExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAuthService), typeof(AuthService));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();

        return services;
    }
}
