using FluentResults;
using MediatR;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokUser;

public record CreateTikTokUserCommand(string Code, Guid UserId) : IRequest<Result<Guid>>;
