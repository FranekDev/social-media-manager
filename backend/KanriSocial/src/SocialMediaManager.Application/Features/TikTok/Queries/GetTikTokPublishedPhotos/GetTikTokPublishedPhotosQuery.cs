using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokPublishedPhotos;

public record GetTikTokPublishedPhotosQuery(Guid UserId) : IRequest<Result<IEnumerable<TikTokPhotoDto>>>;