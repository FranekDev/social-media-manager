using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.Instagram;

public record InstagramBusinessAccount
{
    public string Id { get; init; }
    public string Username { get; init; }

    [JsonProperty("profile_picture_url")]
    public string ProfilePictureUrl { get; init; }
}

public record InstagramData
{
    [JsonProperty("instagram_business_account")]
    public InstagramBusinessAccount InstagramBusinessAccount { get; init; }
    public string Id { get; init; }
}

public record InstagramUserDataResponse
{
    public List<InstagramData> Data { get; init; }
    public InstagramPaging Paging { get; init; }
}