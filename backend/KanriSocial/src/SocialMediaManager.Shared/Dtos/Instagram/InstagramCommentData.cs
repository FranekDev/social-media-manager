namespace SocialMediaManager.Shared.Dtos.Instagram;

public record InstagramCommentData
{
    public IEnumerable<InstagramComment> Data { get; init; }
}

public record InstagramComment
{
    public DateTime Timestamp { get; init; }
    public string Text { get; init; }
    public string Id { get; init; }
}