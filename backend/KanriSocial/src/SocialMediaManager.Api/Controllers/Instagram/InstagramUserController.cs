using System.Security.Claims;
using FluentResults;
using SocialMediaManager.Application.Features.Instagram.User.Commands.CreateInstagramUser;
using SocialMediaManager.Application.Features.Instagram.User.Queries.GetInstagramUser;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaManager.Infrastructure.Clients.Interfaces;

namespace SocialMediaManager.Api.Controllers.Instagram;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InstagramUserController(ISender sender, IInstagramClient instagramClient) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IInstagramClient _instagramClient = instagramClient;

    [HttpPost]
    public async Task<IActionResult> CreateInstagramUser([FromBody] CreateInstagramUserRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Błędne id użytkownika");
        }
        
        var result = await _sender.Send(new CreateInstagramUserCommand(userId.Value, request.Token));
        if (result is null)
        {
            return BadRequest("Błąd podczas tworzenia użytkownika");
        }
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<Result<InstagramUserDetail>>> GetInstagramUserById()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var result = await _sender.Send(new GetInstagramUserByUserIdQuery(userId.Value));
        
        if (result.IsFailed)
        {
            return NotFound(result.Errors);
        }
        
        return Ok(result.Value);
    }

    [HttpPost("user-id")]
    public async Task<ActionResult<Result<InstagramUserDataResponse>>> GetIgUserId([FromBody] string token)
    {
        var userDAta = await _instagramClient.GetUserData(token);
        if (userDAta.IsFailed)
        {
            return BadRequest(userDAta.Errors);
        }
        
        return Ok(userDAta.Value);
    }
    
    private Guid? GetUserId()
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userId, out var userIdGuid))
        {
            return userIdGuid;
        }

        return null;
    }
}