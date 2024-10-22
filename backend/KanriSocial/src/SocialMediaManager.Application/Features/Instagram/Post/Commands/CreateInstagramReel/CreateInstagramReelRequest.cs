namespace SocialMediaManager.Application.Features.Instagram.Post.Commands.CreateInstagramReel;

public record CreateInstagramReelRequest(string VideoBytes, string? Caption, DateTime ScheduledAt);