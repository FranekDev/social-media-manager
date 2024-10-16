using KanriSocial.Application.Services.Interfaces;
using KanriSocial.Domain.Dtos.Account;
using KanriSocial.Domain.Models;
using KanriSocial.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanriSocial.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(
    UserManager<User> userManager, 
    ITokenService tokenService, 
    SignInManager<User> signInManager) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly SignInManager<User> _signInManager = signInManager;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);
            
            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password!");
            }
            
            return Ok(
                new NewUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
            
            var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
            
            if (!createdUser.Succeeded)
            {
                return BadRequest(createdUser.Errors);
            }
            
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }
            
            return Ok(
                new NewUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}