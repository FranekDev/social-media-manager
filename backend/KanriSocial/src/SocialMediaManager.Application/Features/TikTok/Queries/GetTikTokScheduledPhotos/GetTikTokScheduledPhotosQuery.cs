using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokScheduledPhotos;

public record GetTikTokScheduledPhotosQuery(Guid UserId) : IRequest<Result<IEnumerable<TikTokPhotoDto>>>;