using System.Security.Claims;
using KanriSocial.Application.Features.Instagram.Post.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanriSocial.Api.Controllers.Instagram;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InstagramPostController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateInstagramPost([FromBody] CreateInstagramPostCommand command)
    {
        // if (Guid.TryParse(User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var userId))
        // {
        //     // command.UserId = userId;
        // }
        
        var result = await _sender.Send(command);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok(new { PostId = result.Value });
    }
}