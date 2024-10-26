namespace SocialMediaManager.Application.Features.Facebook.Page.Commands.CreateFacebookPagePost;

public record CreateFacebookPagePostRequest(string PageId, string Message, DateTime ScheduledAt);