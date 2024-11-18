namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokContentPostInfo(string Title, string Description, bool DisableComments, string PrivacyLevel, bool AutoAddMusic);