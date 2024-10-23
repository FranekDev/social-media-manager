using SocialMediaManager.Shared.Enums.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramMediaInsight;

public record GetInstagramMediaInsightRequest
{
    public string MediaId { get; init; }
    public InstagramMediaInsightType InsightType { get; init; }
}