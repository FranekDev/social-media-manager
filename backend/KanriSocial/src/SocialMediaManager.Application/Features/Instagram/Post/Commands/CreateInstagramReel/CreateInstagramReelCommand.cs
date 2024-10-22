using FluentResults;
using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.Post.Commands.CreateInstagramReel;

public record CreateInstagramReelCommand(string VideoBytes, string? Caption, DateTime ScheduledAt, Guid UserId) : IRequest<Result<Guid>>;