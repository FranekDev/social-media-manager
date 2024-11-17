namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokVideo;

public record CreateTikTokVideoRequest(string Title, string VideoBytes, DateTime ScheduledAt);