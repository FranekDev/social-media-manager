using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokUser;
using SocialMediaManager.Application.Features.TikTok.Commands.UpdateTikTokUserToken;
using SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokUserInfo;

namespace SocialMediaManager.Api.Controllers.TikTok;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TikTokUserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateTikTokUser([FromBody] CreateTikTokUserRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var result = await _sender.Send(new CreateTikTokUserCommand(request.Code, userId.Value));
        return Ok(result);
    }
    
    [HttpGet("info")]
    public async Task<IActionResult> GetTikTokUserInfo()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var result = await _sender.Send(new GetTikTokUserInfoQuery(userId.Value));
        return Ok(result.Value);
    }
    
    [HttpPost("token")]
    public async Task<IActionResult> UpdateTikTokUserToken([FromBody] UpdateTikTokUserTokenRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Zły user id");
        }
        
        var result = await _sender.Send(new UpdateTikTokUserTokenCommand(request.Code, userId.Value));
        return Ok(result);
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