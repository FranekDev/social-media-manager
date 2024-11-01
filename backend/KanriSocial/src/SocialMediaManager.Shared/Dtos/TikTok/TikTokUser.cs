using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokUser(
    [JsonProperty("avatar_url")] Uri AvatarUrl,
    [JsonProperty("open_id")] string OpenId,
    [JsonProperty("union_id")] string UnionId,
    [JsonProperty("display_name")] string DisplayName);