using System.Security.Claims;
using FluentResults;
using SocialMediaManager.Application.Features.Instagram.User.Commands.CreateInstagramUser;
using SocialMediaManager.Application.Features.Instagram.User.Queries.GetInstagramUser;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaManager.Api.Controllers.Instagram;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InstagramUserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateInstagramUser([FromBody] CreateInstagramUserCommand command)
    {
        try
        {
            var result = await _sender.Send(command);
            return Ok(new { UserId = result });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
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