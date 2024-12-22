using System.Collections;
using System.Security.Claims;
using FluentResults;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaManager.Application.Features.Facebook.Page.Commands.CreateFacebookPagePost;
using SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPageData;
using SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPagePostComments;
using SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPagePublishedPosts;
using SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookScheduledPosts;
using SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookUserPages;
using SocialMediaManager.Application.Features.Facebook.User.Commands.CreateFacebookUser;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Api.Controllers.Facebook;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FacebookUserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateFacebookUser(CreateFacebookUserRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var response = await _sender.Send(new CreateFacebookUserCommand(request.FacebookUserId, request.Token, userId.Value));
        
        if (response.IsFailed)
        {
            return BadRequest(response.Errors);
        }
        
        return Ok(new { UserId = response.Value });
    }

    [HttpGet("{pageId}")]
    public async Task<ActionResult<Result<FacebookPageData>>> GetPageData([FromRoute] string pageId)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var response = await _sender.Send(new GetFacebookPageDataQuery(pageId, userId.Value));

        if (response.IsFailed)
        {
            return BadRequest(response.Errors);
        }

        return Ok(response.Value);
    }
    
    [HttpPost("post")] 
    public async Task<IActionResult> PublishPagePost([FromBody] CreateFacebookPagePostRequest request)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var result = await _sender.Send(new CreateFacebookPagePostCommand(request.PageId, request.Message, request.ScheduledAt, userId.Value));
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(new { PagePostId = result.Value });
    }
    
    [HttpGet("{pageId}/published-posts")]
    public async Task<ActionResult<Result<IEnumerable<FacebookPublishedPost>>>> GetPublishedPosts([FromRoute] string pageId)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var response = await _sender.Send(new GetFacebookPagePublishedPostsQuery(pageId, userId.Value));
        if (response.IsFailed)
        {
            return BadRequest(response.Errors);
        }
        
        return Ok(response.Value);
    }
    
    [HttpPost("{pageId}/{postId}/comments")]
    public async Task<ActionResult<Result<IEnumerable<FacebookPagePostComment>>>> GetPostComments(
        [FromRoute] string pageId, 
        [FromRoute] string postId)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Invalid user id");
        }
        
        var response = await _sender.Send(new GetFacebookPagePostCommentsQuery(pageId, postId, userId.Value));
        if (response.IsFailed)
        {
            return BadRequest(response.Errors);
        }
        
        return Ok(response.Value);
    }

    [HttpGet("pages")]
    public async Task<ActionResult<Result<IEnumerable<FacebookUserPage>>>> GetUserPages()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Błędny user id");
        }

        var response = await _sender.Send(new GetFacebookUserPagesQuery(userId.Value));

        if (response.IsFailed)
        {
            return BadRequest(response.Errors);
        }

        return Ok(response.Value);
    }
    
    [HttpGet("scheduled")]
    public async Task<ActionResult<Result<IEnumerable<FacebookFeedPostDto>>>> GetScheduledPosts()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return BadRequest("Błędny user id");
        }
        
        var response = await _sender.Send(new GetFacebookScheduledPostsQuery(userId.Value));
        return Ok(response);
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