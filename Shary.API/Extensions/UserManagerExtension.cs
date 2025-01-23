using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shary.Core.Entities.Identity;
using System.Security.Claims;

namespace Shary.API.Extensions;

public static class UserManagerExtension
{
    public static async Task<AppUser> FindUserWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U => U.Email == email);
        return user;
    }
}
