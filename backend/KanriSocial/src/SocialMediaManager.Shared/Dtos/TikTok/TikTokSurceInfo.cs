using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokSurceInfo(
    string Source,
    [JsonProperty("video_url")] string VideoUrl
);