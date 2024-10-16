using System.Security.Claims;
using KanriSocial.Application.Features.Instagram.User.Commands;
using KanriSocial.Application.Features.Instagram.User.Commands.CreateInstagramUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KanriSocial.Api.Controllers.Instagram;

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
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //command.UserId = Guid.Parse(userId);
            var result = await _sender.Send(command);
            return Ok(new { UserId = result });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }
}