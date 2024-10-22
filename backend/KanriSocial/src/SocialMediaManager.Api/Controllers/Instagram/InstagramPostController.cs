using System.Security.Claims;
using FluentResults;
using SocialMediaManager.Application.Features.Instagram.Post.Commands.CreateInstagramPost;
using SocialMediaManager.Application.Features.Instagram.Post.Queries.GetInstagramUserPosts;
using SocialMediaManager.Application.Features.Instagram.Post.Queries.GetUnpublishedPosts;
using SocialMediaManager.Domain.Dtos.Instagram;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaManager.Api.Controllers.Instagram;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InstagramPostController(ISender sender) : ControllerBase
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
        
        var result = await _sender.Send(new CreateInstagramPostCommand(request.ImageBytes, request.Caption, request.ScheduledAt, userIdGuid));
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
}