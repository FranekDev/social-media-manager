using System.Security.Claims;
using FluentResults;
using KanriSocial.Application.Features.Instagram.Comment.Queries.GetInstagramCommentReplies;
using KanriSocial.Application.Features.Instagram.Comment.Queries.GetInstagramPostComments;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KanriSocial.Api.Controllers.Instagram;

[ApiController]
[Route("api/[controller]")]
public class InstagramCommentController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpGet("{mediaId}/comments")]
    public async Task<ActionResult<Result<IEnumerable<InstagramComment>>>> GetMediaComments([FromRoute] string mediaId)
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetInstagramPostCommentsQuery(mediaId, userIdGuid));
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result?.Value ?? []);
    }
    
    [HttpGet("{commentId}/replies")]
    public async Task<ActionResult<Result<IEnumerable<InstagramComment>>>> GetCommentReplies([FromRoute] string commentId)
    {
        var userId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userId, out var userIdGuid))
        {
            return BadRequest("Invalid user id");
        }

        var result = await _sender.Send(new GetInstagramCommentRepliesQuery(commentId, userIdGuid));
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(result?.Value ?? []);
    }
}