using Newtonsoft.Json;

namespace KanriSocial.Shared.Dtos.Instagram;

public record InstagramTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
};