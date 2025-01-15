
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shary.Core.Entities.Identity;
using Shary.Core.Services.Contract;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Shary.Service;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.GivenName, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };
        var userRoles = await userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }
        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SeceretKey"]));
        var token = new JwtSecurityToken(
            audience: _configuration["JWT:ValidAudience"],
            issuer: _configuration["JWT:ValidIssuer"],
            expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
