using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokVideo(
    [JsonProperty("title")] string Title,
    [JsonProperty("cover_image_url")] string CoverImageUrl,
    [JsonProperty("id")] string Id  
);

public record TikTokVideos(IEnumerable<TikTokVideo> Videos);
