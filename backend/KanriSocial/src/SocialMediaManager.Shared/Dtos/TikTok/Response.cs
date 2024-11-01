namespace SocialMediaManager.Shared.Dtos.TikTok;

public record Response<T>(T Data, ResponseError Error);