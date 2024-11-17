using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokPostVideoFromUrl(
    [JsonProperty("publish_id")] string PublishId
);

