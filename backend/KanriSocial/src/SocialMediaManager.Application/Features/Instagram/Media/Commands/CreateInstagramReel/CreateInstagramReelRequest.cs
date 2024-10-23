namespace SocialMediaManager.Application.Features.Instagram.Media.Commands.CreateInstagramReel;

public record CreateInstagramReelRequest(string VideoBytes, string? Caption, DateTime ScheduledAt);