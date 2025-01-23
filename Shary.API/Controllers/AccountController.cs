
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shary.API.Dtos;
using Shary.API.Errors;
using Shary.API.Extensions;
using Shary.Core.Entities.Identity;
using Shary.Core.Services.Contract;
using System.Security.Claims;

namespace Shary.API.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IAuthService authService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
        _mapper = mapper;
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if(user ==  null) 
            return Unauthorized(new ApiResponse(401));
        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if(!result.Succeeded)
            return Unauthorized(new ApiResponse(401));

        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });
    }
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto model)
    {
        var user = new AppUser
        {
            DisplayName = model.DisplayName,
            Email = model.Email,
            UserName = model.Email.Split("@")[0],
            PhoneNumber = model.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return BadRequest(new ApiResponse(400));
        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByEmailAsync(email);
        return Ok(new UserDto()
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _authService.CreateTokenAsync(user, _userManager)
        });
    }
    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {
        var user = await _userManager.FindUserWithAddressAsync(User);
        var address = _mapper.Map<AddressDto>(user.Address);
        return Ok(address);
    }
}
