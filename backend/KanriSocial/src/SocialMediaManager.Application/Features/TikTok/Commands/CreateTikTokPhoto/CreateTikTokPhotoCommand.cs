using FluentResults;
using MediatR;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokPhoto;

public record CreateTikTokPhotoCommand(string Title, string Descriptiton, List<string> ImagesBytes, DateTime ScheduledAt, Guid UserId) : IRequest<Result<Guid>>;