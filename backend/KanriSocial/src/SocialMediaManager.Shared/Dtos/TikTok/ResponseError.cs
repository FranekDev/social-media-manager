using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record ResponseError(
    string Code,
    string Message,
    [JsonProperty("log_id")] string LogId);