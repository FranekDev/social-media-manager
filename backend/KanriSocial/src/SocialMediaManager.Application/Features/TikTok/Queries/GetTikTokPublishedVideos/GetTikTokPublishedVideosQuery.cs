using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokPublishedVideos;

public record GetTikTokPublishedVideosQuery(Guid UserId) : IRequest<Result<IEnumerable<TikTokVideoDto>>>;