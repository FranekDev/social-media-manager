using Newtonsoft.Json;

namespace KanriSocial.Shared.Dtos.Instagram;

public record InstagramUserDetail
{
    public string UserName { get; init; }

    public string Biography { get; init; }

    [JsonProperty("media_count")]
    public int MediaCount { get; init; }

    [JsonProperty("followers_count")]
    public int FollowersCount { get; init; }

    [JsonProperty("follows_count")]
    public int FollowsCount { get; init; }

    public string Name { get; init; }

    [JsonProperty("profile_picture_url")]
    public Uri ProfilePictureUrl { get; init; }

    public Uri Website { get; init; }
}