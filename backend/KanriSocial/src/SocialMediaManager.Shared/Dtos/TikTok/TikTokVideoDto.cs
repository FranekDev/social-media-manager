namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokVideoDto
{
    public Guid Id { get; init; }
    public string TikTokId { get; init; }
    public string Title { get; init; }
    public string VideoUrl { get; init; }
    public DateTime ScheduledAt { get; init; }
}