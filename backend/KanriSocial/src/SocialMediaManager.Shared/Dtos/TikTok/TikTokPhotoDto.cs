namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokPhotoDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public List<string> PhotoUrls { get; init; }
    public DateTime ScheduledAt { get; init; }
}