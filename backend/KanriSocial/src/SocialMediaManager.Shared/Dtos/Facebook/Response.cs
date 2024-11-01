namespace SocialMediaManager.Shared.Dtos.Facebook;

public record Response<T>(IEnumerable<T> Data, Paging Paging);