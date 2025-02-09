using FluentResults;
using MediatR;

namespace SocialMediaManager.Application.Features.TikTok.Commands.UpdateTikTokUserToken;

public record UpdateTikTokUserTokenCommand(string Code, Guid UserId) : IRequest<Result>;
