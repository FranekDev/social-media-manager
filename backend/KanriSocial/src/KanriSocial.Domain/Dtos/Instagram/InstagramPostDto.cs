namespace KanriSocial.Domain.Dtos.Instagram;

public record InstagramPostDto(Guid Id, string ImageUrl, string Caption, DateTime ScheduledAt, Guid UserId, bool IsPublished);