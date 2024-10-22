using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.Instagram;

public record InstagramTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
};