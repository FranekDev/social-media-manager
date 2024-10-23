namespace SocialMediaManager.Shared.Dtos.Instagram;

public record InstagramMediaInsight(
    string Name,
    string Period,
    List<InsightValue> Values,
    string Title,
    string Description,
    string Id);