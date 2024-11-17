using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokUser(
    [JsonProperty("avatar_url")] Uri AvatarUrl,
    [JsonProperty("open_id")] string OpenId,
    [JsonProperty("union_id")] string UnionId,
    [JsonProperty("display_name")] string DisplayName,
    [JsonProperty("bio_description")] string BioDescription,
    [JsonProperty("username")] string Username,
    [JsonProperty("follower_count")] int FollowerCount,
    [JsonProperty("following_count")] int FollowingCount,
    [JsonProperty("likes_count")] int LikesCount,
    [JsonProperty("video_count")] int VideoCount,
    [JsonProperty("is_verified")] bool IsVerified);