using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Instagram;
using SocialMediaManager.Shared.Enums.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramMediaInsight;

public record GetInstagramMediaInsightQuery(string MediaId, InstagramMediaInsightType InsightType, Guid UserId) : IRequest<Result<InstagramMediaInsightsData?>>;