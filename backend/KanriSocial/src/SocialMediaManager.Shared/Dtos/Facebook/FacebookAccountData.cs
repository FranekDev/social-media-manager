using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.Facebook;

public record FacebookAccountData(
    [JsonProperty("access_token")] string AccessToken,
    string Category,
    [JsonProperty("category_list")] List<Category> CategoryList,
    string Name,
    string Id,
    List<string> Tasks);
    