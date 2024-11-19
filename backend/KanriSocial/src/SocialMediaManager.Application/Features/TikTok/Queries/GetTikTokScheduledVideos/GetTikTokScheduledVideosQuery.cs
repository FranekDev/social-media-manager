using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokScheduledVideos;

public record GetTikTokScheduledVideosQuery(Guid UserId) : IRequest<Result<IEnumerable<TikTokVideoDto>>>;