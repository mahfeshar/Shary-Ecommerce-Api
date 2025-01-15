
using Microsoft.AspNetCore.Identity;
using Shary.Core.Entities.Identity;

namespace Shary.Core.Services.Contract;

public interface IAuthService
{
    Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
}
