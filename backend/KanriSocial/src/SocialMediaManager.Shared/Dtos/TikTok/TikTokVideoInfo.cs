using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokVideoInfo(
    [JsonProperty("title")] string Title,
    [JsonProperty("id")] string Id,
    [JsonProperty("like_count")] int LikeCount,
    [JsonProperty("comment_count")] int CommentCount,
    [JsonProperty("share_count")] int ShareCount,
    [JsonProperty("view_count")] int ViewCount
);

public record TikTokVideosInfo(IEnumerable<TikTokVideoInfo> Videos);
