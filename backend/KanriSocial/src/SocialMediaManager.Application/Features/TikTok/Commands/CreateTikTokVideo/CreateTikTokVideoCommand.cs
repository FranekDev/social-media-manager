using FluentResults;
using MediatR;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokVideo;

public record CreateTikTokVideoCommand(string Title, string VideoBytes, DateTime ScheduledAt, Guid UserId) : IRequest<Result<Guid>>;
