using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.Facebook;

public record FacebookPublishedPost(
    [JsonProperty("created_time")] DateTime CreatedTime,
    string Message,
    string Id);