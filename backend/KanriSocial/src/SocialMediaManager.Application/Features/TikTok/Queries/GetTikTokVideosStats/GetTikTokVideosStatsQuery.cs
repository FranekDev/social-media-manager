using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokVideosStats;

public record GetTikTokVideosStatsQuery(Guid UserId) : IRequest<Result<IEnumerable<TikTokVideoInfo>>>;