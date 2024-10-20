using System.Security.Claims;
using KanriSocial.Application.Services.Interfaces;
using KanriSocial.Domain.Dtos.Account;
using KanriSocial.Domain.Models;
using KanriSocial.Infrastructure.Database;
using KanriSocial.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    SignInManager<User> signInManager,
    UserTokenRepository userTokenRepository) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserTokenRepository _userTokenRepository = userTokenRepository;

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
            var existingToken = await _userTokenRepository.GetByUserId(user.Id);
            string token;
            if (existingToken != null)
            {
                await _userTokenRepository.Delete(user.Id);
                token = await _tokenService.CreateToken(user);
            }
            else
            {
                token = existingToken.Token;
            }
            
            // var token = await _tokenService.CreateToken(user);
            var newUserDto = new NewUserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token
            };
            
            return Ok(newUserDto);
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
            
            var token = await _tokenService.CreateToken(user);
            var newUserDto = new NewUserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = token
            };
            
            return Ok(newUserDto);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            
            var userToken = await _userTokenRepository.GetByUserId(userId);
            
            if (userToken == null)
            {
                return BadRequest("User token not found");
            }
            
            if (userToken.UserId != userId)
            {
                return Unauthorized("Invalid user token");
            }

            userToken.IsValid = false;
            await _userTokenRepository.Update(userToken);
            
            return Ok("User logged out successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}