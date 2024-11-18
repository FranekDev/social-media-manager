namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokPhoto;

public record CreateTikTokPhotoRequest(string Title, string Descriptiton, List<string> ImagesBytes, DateTime ScheduledAt);