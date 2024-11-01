using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokToken(
    [JsonProperty("access_token")] string AccessToken,
    [JsonProperty("expires_in")] int ExpiresIn,
    [JsonProperty("open_id")] string OpenId,
    [JsonProperty("refresh_expires_in")] int RefreshExpiresIn,
    [JsonProperty("refresh_token")] string RefreshToken,
    [JsonProperty("scope")] string Scope,
    [JsonProperty("token_type")] string TokenType);