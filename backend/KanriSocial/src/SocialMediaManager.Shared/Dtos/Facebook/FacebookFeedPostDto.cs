namespace SocialMediaManager.Shared.Dtos.Facebook;

public record FacebookFeedPostDto(Guid? Id, string PageId, string Message, DateTime ScheduledAt);