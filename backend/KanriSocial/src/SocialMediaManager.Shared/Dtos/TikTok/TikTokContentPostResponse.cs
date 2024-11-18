using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokContentPostResponse([JsonProperty("publish_id")] string PublishId);