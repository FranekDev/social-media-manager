using System.Security.Claims;
using FluentResults;
using SocialMediaManager.Domain.Dtos.Instagram;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaManager.Application.Features.Instagram.Media.Commands.CreateInstagramPost;
using SocialMediaManager.Application.Features.Instagram.Media.Commands.CreateInstagramReel;
using SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramMediaInsight;
using SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramUserPosts;
using SocialMediaManager.Application.Features.Instagram.Media.Queries.GetUnpublishedPosts;
using SocialMediaManager.Application.Features.Instagram.Media.Queries.GetUnpublishedReels;

namespace SocialMediaManager.Api.Controllers.Instagram;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InstagramMediaController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateInstagramPost([FromBody] CreateInstagramPostRequest request)
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new CreateInstagramPostCommand(request.ImageBytes, request.Caption,
            request.ScheduledAt, userIdGuid));

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { PostId = result.Value });
    }

    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<InstagramMediaDetail>>>> GetInstagramPosts(
        [FromQuery(Name = "pageNumber")] int pageNumber,
        [FromQuery(Name = "pageSize")] int pageSize)
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetInstagramUserPostsQuery(userIdGuid, pageNumber, pageSize));

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpGet("unpublished")]
    public async Task<ActionResult<Result<IEnumerable<InstagramPostDto>>>> GetUnpublishedPosts()
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetUnpublishedPostsQuery(userIdGuid));

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpPost("reel")]
    public async Task<IActionResult> CreateInstagramReelPost([FromBody] CreateInstagramReelRequest request)
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new CreateInstagramReelCommand(request.VideoBytes, request?.Caption,
            request.ScheduledAt, userIdGuid));

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { PostId = result.Value });
    }

    [HttpPost("insights")]
    public async Task<ActionResult<Result<InstagramMediaInsightsData>>> GetInstagramMediaInsight(
        [FromBody] GetInstagramMediaInsightRequest request)
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }
        
        var result = await _sender.Send(new GetInstagramMediaInsightQuery(request.MediaId, request.InsightType, userIdGuid));
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("unpublished/reels")]
    public async Task<ActionResult<IEnumerable<InstagramReelDto>>> GetUnpublishedReels()
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetUnpublishedReelsQuery(userIdGuid));
        return Ok(result);
    }
}