namespace SocialMediaManager.Shared.Dtos.Instagram;

public record InstagramReelDto
{
    public Guid Id { get; set; }
    public string VideoUrl { get; set; }
    public string? Caption { get; set; }
    public DateTime ScheduledAt { get; set; }
}