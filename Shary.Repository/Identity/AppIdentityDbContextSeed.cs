
using Microsoft.AspNetCore.Identity;
using Shary.Core.Entities.Identity;

namespace Shary.Repository.Identity;

public class AppIdentityDbContextSeed
{
    public async static Task SeedUsersAsync(UserManager<AppUser> _userManage)
    {
        if(_userManage.Users.Count() == 0)
        {
            var user = new AppUser
            {
                DisplayName = "Mahmoud Feshar",
                Email = "mahmoudfeshar11@gmail.com",
                UserName = "mahfeshar",
                PhoneNumber = "01289086935"
            };
            await _userManage.CreateAsync(user, "Pa$$w0rd");
        }
    }
}
