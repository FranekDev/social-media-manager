namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokPhoto;

public record CreateTikTokPhotoRequest(string Title, string Description, List<string> ImagesBytes, DateTime ScheduledAt);