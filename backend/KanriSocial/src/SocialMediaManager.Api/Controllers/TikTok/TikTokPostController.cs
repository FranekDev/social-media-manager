using System.Security.Claims;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokPhoto;
using SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokVideo;
using SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokPublishedPhotos;
using SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokPublishedVideos;
using SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokScheduledPhotos;
using SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokScheduledVideos;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Api.Controllers.TikTok;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TikTokPostController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("videos")]
    public async Task<IActionResult> CreateTikTokVideo([FromBody] CreateTikTokVideoRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new CreateTikTokVideoCommand(request.Title, request.VideoBytes, request.ScheduledAt, userId.Value));
        return Ok(new { VideoId = result.Value});
    }
    
    [HttpPost("photos")]
    public async Task<IActionResult> CreateTikTokPhoto([FromBody] CreateTikTokPhotoRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new CreateTikTokPhotoCommand(request.Title, request.Descriptiton, request.ImagesBytes, request.ScheduledAt, userId.Value));
        return Ok(new { PhotoId = result.Value});
    }
    
    [HttpGet("videos/scheduled")]
    public async Task<IActionResult> GetScheduledVideos()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetTikTokScheduledVideosQuery(userId.Value));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        
        return BadRequest(result.Errors);
    }
    
    [HttpGet("photos/scheduled")]
    public async Task<IActionResult> GetScheduledPhotos()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetTikTokScheduledPhotosQuery(userId.Value));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        
        return BadRequest(result.Errors);
    }
    
    [HttpGet("photos/published")]
    public async Task<ActionResult<Result<IEnumerable<TikTokPhotoDto>>>> GetPublishedPhotos()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetTikTokPublishedPhotosQuery(userId.Value));
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("videos/published")]
    public async Task<ActionResult<Result<IEnumerable<TikTokVideoDto>>>> GetPublishedVideos()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetTikTokPublishedVideosQuery(userId.Value));
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
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