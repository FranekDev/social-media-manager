using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokVideo;

namespace SocialMediaManager.Api.Controllers.TikTok;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TikTokPostController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateTikTokPost([FromBody] CreateTikTokVideoRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new CreateTikTokVideoCommand(request.Title, request.VideoBytes, request.ScheduledAt, userId.Value));
        return Ok(new { VideoId = result.Value});
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