using KanriSocial.Application.Services.Interfaces;
using KanriSocial.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace KanriSocial.Api.Middlewares;

public class TokenMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    
    public async Task InvokeAsync(HttpContext context, ITokenService tokenService, UserTokenRepository userTokenRepository)
    {
        var endpoint = context.GetEndpoint();
        var authorizeAttribute = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>();

        if (authorizeAttribute is null)
        {
            await _next(context);
            return;
        }
        
        if (!context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authorization header missing");
            return;
        }

        token = token.ToString().Split(" ").Last();
        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token missing");
            return;
        }

        var userId = await tokenService.ValidateToken(token);
        if (userId is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }
        
        var userToken = await userTokenRepository.GetByUserId(userId);
        if (userToken is null || userToken.Token != token || !userToken.IsValid)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }
        
        await _next(context);
    }
}