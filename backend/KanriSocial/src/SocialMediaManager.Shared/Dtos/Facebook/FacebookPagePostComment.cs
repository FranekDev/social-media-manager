using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.Facebook;

public record FacebookPagePostComment(
    [JsonProperty("created_time")] DateTime CreatedTime,
    From From,
    string Message,
    string Id);
